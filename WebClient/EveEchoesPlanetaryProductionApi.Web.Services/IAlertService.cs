namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System;

    using EveEchoesPlanetaryProductionApi.Web.Common;

    public interface IAlertService
    {
        event Action<AlertModel> OnAlert;

        void Success(string message, bool keepAfterRouteChange = false, bool autoClose = true);

        void Error(string message, bool keepAfterRouteChange = false, bool autoClose = true);

        void Info(string message, bool keepAfterRouteChange = false, bool autoClose = true);

        void Warn(string message, bool keepAfterRouteChange = false, bool autoClose = true);

        void Alert(AlertModel alert);

        void Clear(string id = null);
    }
}
