namespace iso.gmh.Core.Interfaces;

using System.IO;

using iso.gmh.Core.Models;

public interface IBackupStrategy
{
    /// <summary>
    /// Retorna a extensão do arquivo a ser salvo.
    /// </summary>
    /// <returns>
    /// Texto com a extensão do arquivo.
    /// </returns>
    string GetFileExtension();

    /// <summary>
    /// Prepara o backup de um jogo selecionado.
    /// </summary>
    /// <param name="game">
    /// Jogo ao qual o backup será feito.
    /// </param>
    void PrepareBackup(
        Game game
    );

    /// <summary>
    /// Gera o backup do jogo selecionado.
    /// </summary>
    /// <param name="game">
    /// Jogo ao qual o backup será feito.
    /// </param>
    /// <returns>
    /// Retorna a stream do jogo após o backup realizado.
    /// </returns>
    FileStream GenerateBackup(
        Game game
    );
}