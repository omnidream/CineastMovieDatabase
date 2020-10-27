using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using interaktiva20_2.Models.DTO;

namespace interaktiva20_2.Data
{
    public interface ICMDbRepository
    {
        Task<IEnumerable<TopListDto>> GetToplist();
    }
}
