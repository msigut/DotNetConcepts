using System;
using System.IO;

namespace CsvImporter
{
	public class EscapeToDoubleQuoteReader : TextReader
	{
		private const int CHAR_BACKSLASH = '\\';
		private const int CHAR_DOUBLEQUOTE = '"';

		public EscapeToDoubleQuoteReader(TextReader reader, bool keepOpen = false)
		{
			_reader = reader ?? throw new ArgumentNullException(nameof(reader));
			_keepOpen = keepOpen;

			_buffer = new int[2];
			_buffer[0] = _reader.Read();
			_buffer[1] = _reader.Read();
		}

		private readonly TextReader _reader;
		private readonly bool _keepOpen;

		private readonly int[] _buffer;

		public override int Peek()
		{
			if (_buffer[0] == CHAR_BACKSLASH && _buffer[1] == CHAR_DOUBLEQUOTE)
				return CHAR_DOUBLEQUOTE;

			return _buffer[0];
		}

		public override int Read()
		{
			var ch = Peek();
			_buffer[0] = _buffer[1];
			_buffer[1] = _reader.Read();
			return ch;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && !_keepOpen)
			{
				_reader.Dispose();
			}
		}
	}
}
