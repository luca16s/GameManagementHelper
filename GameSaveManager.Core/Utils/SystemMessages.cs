namespace GameSaveManager.Core.Utils
{
    public static class SystemMessages
    {
        public const string ErrorItemNotFoundOnEnum = "Item não encontrado no enumerador.";
        public const string ErrorSaveExtensionNotSupported = "Extensão de arquivo não suportada.";
        public const string ErrorFileNotSupported = "Arquivo não suportado.";

        public const string SaveNameIsNullMessage = "Nome do save não informado, utilizando nome padrão.";
        public const string SaveNameMinLengthMessage = "Nome do save não pode ter menos do que 5 caracteres.";
        public const string SaveNameMaxLenghtMessage = "Nome do save não pode ter mais do que 150 caracteres.";
    }
}
