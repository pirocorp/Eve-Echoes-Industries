namespace EveEchoesPlanetaryProductionApi.Web.Services
{
    using System;

    using EveEchoesPlanetaryProductionApi.Web.Common;

    public class AlertService : IAlertService
    {
        private const string DefaultId = "default-alert";

        public event Action<AlertModel> OnAlert;

        public void Success(string message, bool keepAfterRouteChange = false, bool autoClose = true)
        {
            this.Alert(new AlertModel
            {
                Type = AlertType.Success,
                Message = message,
                KeepAfterRouteChange = keepAfterRouteChange,
                AutoClose = autoClose,
            });
        }

        public void Error(string message, bool keepAfterRouteChange = false, bool autoClose = true)
        {
            this.Alert(new AlertModel
            {
                Type = AlertType.Error,
                Message = message,
                KeepAfterRouteChange = keepAfterRouteChange,
                AutoClose = autoClose,
            });
        }

        public void Info(string message, bool keepAfterRouteChange = false, bool autoClose = true)
        {
            this.Alert(new AlertModel
            {
                Type = AlertType.Info,
                Message = message,
                KeepAfterRouteChange = keepAfterRouteChange,
                AutoClose = autoClose,
            });
        }

        public void Warn(string message, bool keepAfterRouteChange = false, bool autoClose = true)
        {
            this.Alert(new AlertModel
            {
                Type = AlertType.Warning,
                Message = message,
                KeepAfterRouteChange = keepAfterRouteChange,
                AutoClose = autoClose,
            });
        }

        public void Alert(AlertModel alert)
        {
            alert.Id ??= DefaultId;
            this.OnAlert?.Invoke(alert);
        }

        public void Clear(string id = DefaultId)
        {
            this.OnAlert?.Invoke(new AlertModel { Id = id });
        }
    }
}
