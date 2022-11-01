using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Application.Abstractions.Services
{
    public interface ISmsService
    {
        Task<bool> Send(string recipient, string message);
    }
}
