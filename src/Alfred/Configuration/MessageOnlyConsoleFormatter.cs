using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

namespace Alfred.Configuration;

internal sealed class MessageOnlyConsoleFormatter : ConsoleFormatter, IDisposable
{
    public const string Name = "message-only";

    private readonly IDisposable? _optionsReloadToken;
    private SimpleConsoleFormatterOptions _formatterOptions;

    public MessageOnlyConsoleFormatter(IOptionsMonitor<SimpleConsoleFormatterOptions> options)
        : base(Name)
    {
        _formatterOptions = options.CurrentValue;
        _optionsReloadToken = options.OnChange(updatedOptions => _formatterOptions = updatedOptions);
    }

    public override void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider? scopeProvider, TextWriter textWriter)
    {
        var message = logEntry.Formatter?.Invoke(logEntry.State, logEntry.Exception);

        if (string.IsNullOrEmpty(message))
        {
            return;
        }

        if (_formatterOptions.SingleLine)
        {
            textWriter.Write(message.Replace(Environment.NewLine, " "));
            textWriter.Write(Environment.NewLine);
            return;
        }

        textWriter.Write(message);
        textWriter.Write(Environment.NewLine);
    }

    public void Dispose()
    {
        _optionsReloadToken?.Dispose();
    }
}