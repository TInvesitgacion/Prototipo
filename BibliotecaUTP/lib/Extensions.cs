using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace BibliotecaUTP.lib {
    internal static class Extensions {
        public static bool EqualsIgnoreCase(this string text, string compareTo) {
            if (text == null)
                return compareTo == null;
            return text.Equals(compareTo, StringComparison.OrdinalIgnoreCase);
        }

        public static string GetValue(this XElement element) {
            if (element == null)
                return null;

            return element.Value;
        }

        public static string GetValue(this XElement element, string name) {
            return element?.GetElement(name).GetValue();
        }

        public static string GetImgValue(this XElement element, string name) {
            return element?.GetElement(name).GetElement("img").GetAttributeValue("src");
        }

        public static string GetValue(this XAttribute attribute) {
            if (attribute == null)
                return null;

            return attribute.Value;
        }

        public static string GetAttributeValue(this XElement element, string name) {
            return element.GetAttribute(name)?.GetValue();
        }

        public static XAttribute GetAttribute(this XElement element, string name) {
            var splitted = SplitName(name);
            return element?.GetAttribute(splitted.Item1, splitted.Item2);
        }

        public static XAttribute GetAttribute(this XElement element, string namespacePrefix, string name) {
            if (string.IsNullOrEmpty(namespacePrefix))
                return element.Attribute(name);

            var namesp = element.GetNamespacePrefix(namespacePrefix);
            return element.Attribute(namesp + name);
        }

        public static XElement GetElement(this XElement element, string name) {
            var splitted = SplitName(name);
            return element?.GetElement(splitted.Item1, splitted.Item2);
        }

        public static XElement GetElement(this XElement element, string namespacePrefix, string name) {
            var namesp = element.GetNamespacePrefix(namespacePrefix);
            if (namesp == null) return null;
            return element.Element(namesp + name);
        }

        public static IEnumerable<XElement> GetElements(this XElement element, string name) {
            var splitted = SplitName(name);
            return element.GetElements(splitted.Item1, splitted.Item2);
        }

        public static IEnumerable<XElement> GetElements(this XElement element, string namespacePrefix, string name) {
            var namesp = element.GetNamespacePrefix(namespacePrefix);
            if (namesp == null) return null;
            return element.Elements(namesp + name);
        }

        public static XNamespace GetNamespacePrefix(this XElement element, string namespacePrefix) {
            var namesp = string.IsNullOrWhiteSpace(namespacePrefix) ? element.GetDefaultNamespace() : element.GetNamespaceOfPrefix(namespacePrefix);
            return namesp;
        }

        private static Tuple<string, string> SplitName(string name) {
            string namesp = null;
            if (name.Contains(":")) {
                int pos = name.IndexOf(':');
                namesp = name.Substring(0, pos);
                name = name.Substring(pos + 1);
            }

            return new Tuple<string, string>(namesp, name);
        }
    }
}
