namespace GameSaveManager.Core.Utils
{
    public static class SystemMessages
    {
        public const string ErrorFileNotSupported = "Arquivo não suportado.";
        public const string ErrorItemNotFoundOnEnum = "Item não encontrado no enumerador.";
        public const string ErrorConnectionNotSupported = "Conexão não suportada.";
        public const string ErrorSaveExtensionNotSupported = "Extensão de arquivo não suportada.";

        public const string UserDefinedSaveNameErrorMessage = "Nome do save informado está incorreto.";
        public const string SaveNameMinLengthMessage = "Nome do save não pode ter menos do que 5 caracteres.";
        public const string SaveNameMaxLenghtMessage = "Nome do save não pode ter mais do que 150 caracteres.";

        public const string EmailCannotBeNullMessage = "E-Mail não pode estar nulo.";
        public const string UserNameCannotBeNullMessage = "Nome de usuário não pode estar nulo.";

        public const string MustBeAnEnumTypeMessage = "Tipo esperado: Enum. Tipo recebido: {0}.";
    }
}