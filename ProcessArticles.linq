<Query Kind="Program">
  <NuGetReference>Markdig</NuGetReference>
  <Namespace>Markdig</Namespace>
  <Namespace>Markdig.Extensions.Abbreviations</Namespace>
  <Namespace>Markdig.Extensions.Alerts</Namespace>
  <Namespace>Markdig.Extensions.AutoIdentifiers</Namespace>
  <Namespace>Markdig.Extensions.AutoLinks</Namespace>
  <Namespace>Markdig.Extensions.Bootstrap</Namespace>
  <Namespace>Markdig.Extensions.Citations</Namespace>
  <Namespace>Markdig.Extensions.CustomContainers</Namespace>
  <Namespace>Markdig.Extensions.DefinitionLists</Namespace>
  <Namespace>Markdig.Extensions.Diagrams</Namespace>
  <Namespace>Markdig.Extensions.Emoji</Namespace>
  <Namespace>Markdig.Extensions.EmphasisExtras</Namespace>
  <Namespace>Markdig.Extensions.Figures</Namespace>
  <Namespace>Markdig.Extensions.Footers</Namespace>
  <Namespace>Markdig.Extensions.Footnotes</Namespace>
  <Namespace>Markdig.Extensions.GenericAttributes</Namespace>
  <Namespace>Markdig.Extensions.Globalization</Namespace>
  <Namespace>Markdig.Extensions.Hardlines</Namespace>
  <Namespace>Markdig.Extensions.JiraLinks</Namespace>
  <Namespace>Markdig.Extensions.ListExtras</Namespace>
  <Namespace>Markdig.Extensions.Mathematics</Namespace>
  <Namespace>Markdig.Extensions.MediaLinks</Namespace>
  <Namespace>Markdig.Extensions.NonAsciiNoEscape</Namespace>
  <Namespace>Markdig.Extensions.NoRefLinks</Namespace>
  <Namespace>Markdig.Extensions.PragmaLines</Namespace>
  <Namespace>Markdig.Extensions.ReferralLinks</Namespace>
  <Namespace>Markdig.Extensions.SelfPipeline</Namespace>
  <Namespace>Markdig.Extensions.SmartyPants</Namespace>
  <Namespace>Markdig.Extensions.Tables</Namespace>
  <Namespace>Markdig.Extensions.TaskLists</Namespace>
  <Namespace>Markdig.Extensions.TextRenderer</Namespace>
  <Namespace>Markdig.Extensions.Yaml</Namespace>
  <Namespace>Markdig.Helpers</Namespace>
  <Namespace>Markdig.Parsers</Namespace>
  <Namespace>Markdig.Parsers.Inlines</Namespace>
  <Namespace>Markdig.Renderers</Namespace>
  <Namespace>Markdig.Renderers.Html</Namespace>
  <Namespace>Markdig.Renderers.Html.Inlines</Namespace>
  <Namespace>Markdig.Renderers.Normalize</Namespace>
  <Namespace>Markdig.Renderers.Normalize.Inlines</Namespace>
  <Namespace>Markdig.Renderers.Roundtrip</Namespace>
  <Namespace>Markdig.Renderers.Roundtrip.Inlines</Namespace>
  <Namespace>Markdig.Syntax</Namespace>
  <Namespace>Markdig.Syntax.Inlines</Namespace>
</Query>

void Main()
{
	var baseFolder = Path.GetDirectoryName(Util.CurrentQueryPath);
	var markdownFolder = Path.Combine(baseFolder, "markdown");
	var outputFolder = Path.Combine(baseFolder, "docs");
	var templateFile = Path.Combine(baseFolder, "template/template.html");
	
	string[] files = Directory.GetFiles(markdownFolder);
	var template = File.ReadAllText(templateFile);
	var indexMarkdown = new StringBuilder();
	indexMarkdown.AppendLine("# Articles");
	
	var pipeline = new MarkdownPipelineBuilder().UsePipeTables().Build();
	
	foreach(var file in files)
	{
		var markdown = File.ReadAllText(file);
		var markdownHtml = Markdown.ToHtml(markdown, pipeline);
		
		var outputFileName = Path.GetFileNameWithoutExtension(file);
		var outputFileNameWithExtension = outputFileName + ".html";
		
		var title = ExtractFirstHeading1(file);
		if(title is null)
		{
			title = outputFileName;
		}

		var html = template.Replace("{{title}}", title).Replace("{{body}}", markdownHtml);

		File.WriteAllText(Path.Combine(outputFolder, outputFileNameWithExtension), html);

		// Add to index.
		indexMarkdown.AppendLine($"- [{title}]({outputFileNameWithExtension})");
	}
	
	// Save index file
	var indexHtml = Markdown.ToHtml(indexMarkdown.ToString());
	var indexFullHtml = template.Replace("{{title}}", "Articles").Replace("{{body}}", indexHtml);
	File.WriteAllText(Path.Combine(outputFolder, "index.html"), indexFullHtml);
	
	
	$"Operation completed at: {DateTime.Now}".Dump();
	
}

// You can define other methods, fields, classes and namespaces here
public static string? ExtractFirstHeading1(string markdownFilePath)
{
	if (!File.Exists(markdownFilePath))
		throw new FileNotFoundException("File not found", markdownFilePath);

	string markdown = File.ReadAllText(markdownFilePath);

	// Parse the Markdown into a syntax tree (AST)
	MarkdownDocument document = Markdown.Parse(markdown);

	// Search for the first HeadingBlock with level 1
	foreach (var block in document)
	{
		if (block is HeadingBlock heading && heading.Level == 1)
		{
			return string.Concat(heading.Inline?.Select(x => x.ToString()));
		}
	}

	return null; // No H1 found
}