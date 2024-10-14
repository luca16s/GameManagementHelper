namespace iso.gmh.Core.Interfaces;

using System.Collections.Generic;
using System.Threading.Tasks;

using iso.gmh.Core.Models;

public interface ICloudOperations
{
    /// <summary>
    /// Apaga arquivo do serviço de armazenamento na nuvem.
    /// </summary>
    /// <param name="path">
    /// Local do arquivo a ser apagado.
    /// </param>
    /// <returns>
    /// True caso tenha apagado e Falso caso não tenha sido apagado.
    /// </returns>
    Task<bool> DeleteSave(
        string path
    );

    /// <summary>
    /// Cria uma pasta no serviço de armazenamento na nuvem.
    /// </summary>
    /// <param name="path">
    /// </param>
    /// <returns>
    /// </returns>
    Task<bool> CreateFolder(
        string path
    );

    /// <summary>
    /// Valida se no serviço de armazenamento na nuvem já existe uma pasta com mesmo nome.
    /// </summary>
    /// <param name="folderName">
    /// Nome da pasta.
    /// </param>
    /// <returns>
    /// True se existente e Falso se não existe.
    /// </returns>
    Task<bool> CheckFolderExistence(
        string folderName
    );

    /// <summary>
    /// Realiza o download do save no serviço da nuvem.
    /// </summary>
    /// <param name="game">
    /// Jogo selecionado.
    /// </param>
    /// <returns>
    /// True se o save pode ser recuperado e Falso caso não tenha sido recuperado.
    /// </returns>
    Task<bool> DownloadSaveData(
        Game game
    );

    /// <summary>
    /// Realiza o upload do save para o serviço de armazenamento na nuvem.
    /// </summary>
    /// <param name="game">
    /// Jogo selecionado.
    /// </param>
    /// <param name="overwrite">
    /// Indica se save pode ser sobrescrito.
    /// </param>
    /// <returns>
    /// True caso tenha sido salvo e Falso caso não tenha sido salvo.
    /// </returns>
    Task<bool> UploadSaveData(
        Game game,
        bool overwrite = false
    );

    /// <summary>
    /// Busca a lista de saves armazenados no serviço da nuvem.
    /// </summary>
    /// <param name="game">
    /// Jogo selecionado.
    /// </param>
    /// <returns>
    /// Lista de arquivos salvos.
    /// </returns>
    Task<IEnumerable<(string name, string path)>> GetSavesList(
        Game game
    );
}