using System.Text;

namespace TireService.Core.Utils
{
    public class LTreeConvertHelper
    {
        private const int MaxDuplicationIndex = 9999;

        private const int EntityValueMaxLength = 250;

        private const int DuplicationPostfixMaxLength = 5;

        private static Dictionary<char, char> _symbolsToChars = new()
        {
            { '~', 'а' },
            { '-', 'б' },
            { '+', 'в' },
            { '*', 'г' },
            { '=', 'д' },
            { '/', 'е' },
            { '\\', 'ё' },
            { '|', 'ж' },
            { '!', 'з' },
            { '?', 'и' },
            { '@', 'й' },
            { '#', 'к' },
            { '$', 'л' },
            { '%', 'м' },
            { '&', 'н' },
            { '_', 'о' },
            { ':', 'п' },
            { ';', 'р' },
            { '^', 'с' },
            { '`', 'т' },
            { '\'', 'у' },
            { '"', 'ф' },
            { ',', 'х' },
            { '.', 'ц' },
            { '(', 'ч' },
            { ')', 'ш' },
            { '[', 'щ' },
            { ']', 'ъ' },
            { '{', 'ы' },
            { '}', 'ь' },
            { '<', 'э' },
            { '>', 'ю' },
            { ' ', 'я' },
            { '№', 'b' }
        };

        public static string ConcatPath(string path1, string path2)
        {
            if (string.IsNullOrWhiteSpace(path1))
                return path2;
            return path1 + '.' + path2;
        }

        public static string NormalizeValue(string value, int? duplicationIndex = null)
        {
            if (value.Length > EntityValueMaxLength)
            {
                throw new ArgumentException($"{nameof(value)} length must be lower than or equal {EntityValueMaxLength}");
            }

            if (duplicationIndex > MaxDuplicationIndex)
            {
                throw new ArgumentException($"{nameof(duplicationIndex)} must be lower than or equal {MaxDuplicationIndex}");
            }

            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            StringBuilder stringBuilder = new(value.Length + DuplicationPostfixMaxLength);

            
            foreach (var @char in value)
            {
                stringBuilder.Append(NormalizeChar(@char));
            }

            var result = duplicationIndex.HasValue && duplicationIndex > 0
                ? $"{stringBuilder}_{duplicationIndex:00000}"
                : stringBuilder.ToString();

            return result;
        }

        // Буквы переводим в апперкейс, символы транслируем в буквы в ловеркейсе.
        // Соответственно по кейсу можно понять что перед нами: буква или символ.
        private static char NormalizeChar(char @char)
        {
            if (char.IsLetterOrDigit(@char))
            {
                return char.ToUpper(@char);
            }

            if (_symbolsToChars.TryGetValue(@char, out var normalizedSymbol))
            {
                return normalizedSymbol;
            }

            //Как только введут запрет на альтернативные спецсимволы заменить на
            //throw new ArgumentException($"Unsupported character found: {@char}");
            return 'z';
        }
    }
}
