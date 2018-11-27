using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Command
{
    public enum ReporType
    {
        правонарушение,
        недоработка,
        сбой_программы,
        предложения,
        другое,
    }

    public class Error
    {
        #region Свойства
        /// <summary>
        /// Брошенное исключение.
        /// </summary>
        Exception Exception { get; set; }

        /// <summary>
        /// Было ли отправлена ошибка серверу.
        /// </summary>
        bool Forwarded { get; set; }

        /// <summary>
        /// Дата и время получения исключения.
        /// </summary>
        DateTime Data { get; set; }

        /// <summary>
        /// Дополнительная информация об ошибке
        /// </summary>
        string Information { get; set; }
        #endregion

        #region Конструктор
        public Error(DateTime data, string information, Exception exception)
        {
            Exception = exception;
            Data = data;
            Information = information;
        }


        //public Error(DateTime data, string information)
        //{
        //    Data = data;
        //    Information = information;
        //}
        #endregion
    }
}
