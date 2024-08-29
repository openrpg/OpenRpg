const indentationFinderRegex = new RegExp("^(?:[\\n])([ ]+){1}");
const htmlEncodingFixRegex = new RegExp("&amp;", "g");

window.toMarkdown = function (element) {
    const converter = new showdown.Converter({ extensions: ['highlight'] });
    const unprocessedText = element.firstChild.value;

    const matches = unprocessedText.match(indentationFinderRegex);
    const firstLineIndentationAmount = matches[1].length;

    const indentationRemovalRegex = new RegExp(`^[ ]{${firstLineIndentationAmount}}`, "gm");
    const text = element.firstChild.value.replace(indentationRemovalRegex, "");
    const html = converter.makeHtml(text).replace(htmlEncodingFixRegex, "&");
    element.innerHTML = html;
}

hljs.initHighlightingOnLoad();

showdown.extension('highlight', function () {
    return [{
        type: "output",
        filter: function (text, converter, options) {
            var left = "<pre><code\\b[^>]*>",
                right = "</code></pre>",
                flags = "g";
            var replacement = function (wholeMatch, match, left, right) {
                var lang = (left.match(/class=\"([^ \"]+)/) || [])[1];
                left = left.slice(0, 18) + 'hljs ' + left.slice(18);
                if (lang && hljs.getLanguage(lang)) {
                    return left + hljs.highlight(lang, match).value + right;
                } else {
                    return left + hljs.highlightAuto(match).value + right;
                }
            };
            return showdown.helper.replaceRecursiveRegExp(text, replacement, left, right, flags);
        }
    }];
});