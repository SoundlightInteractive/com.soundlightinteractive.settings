using System;

namespace SoundlightInteractive.Settings
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ResourcePathAttribute : Attribute
    {
        public string path { get; private set; }

        public ResourcePathAttribute(string path)
        {
            this.path = path;
        }
    }
}