using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Serialization;

namespace BearBlog.Models
{
    public class VisibilityContractResolver : CamelCasePropertyNamesContractResolver
    {
        private readonly Visibility _visibility;

        public VisibilityContractResolver(Visibility visibility)
        {
            this._visibility = visibility;
        }

        protected override List<MemberInfo> GetSerializableMembers(Type objectType)
        {
            return base.GetSerializableMembers(objectType)
                .Where(mi =>
                    mi.GetCustomAttribute<VisibilityAttribute>() == null ||
                    mi.GetCustomAttribute<VisibilityAttribute>().Visibility <= _visibility)
                .ToList();
        }
    }
}