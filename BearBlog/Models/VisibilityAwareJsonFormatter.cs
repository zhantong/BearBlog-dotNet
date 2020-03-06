using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace BearBlog.Models
{
    public class VisibilityAwareJsonFormatter : TextOutputFormatter
    {
        public VisibilityAwareJsonFormatter()
        {
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/json"));
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context,
            Encoding selectedEncoding)
        {
            var visibility = Visibility.Full;
            if (context.HttpContext.Items.TryGetValue(VisibilityFilterAttribute.HttpContextItemKey, out var item)
                && item is VisibilityFilterAttribute attribute)
            {
                visibility = attribute.Visibility;
            }

            var response = context.HttpContext.Response;
            await using var writer = context.WriterFactory(response.Body, selectedEncoding);
            await writer.WriteAsync(JsonConvert.SerializeObject(context.Object,
                new JsonSerializerSettings()
                {
                    ContractResolver = new VisibilityContractResolver(visibility),
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }));

            // Perf: call FlushAsync to call WriteAsync on the stream with any content left in the TextWriter's
            // buffers. This is better than just letting dispose handle it (which would result in a synchronous
            // write).
            await writer.FlushAsync();
        }
    }
}