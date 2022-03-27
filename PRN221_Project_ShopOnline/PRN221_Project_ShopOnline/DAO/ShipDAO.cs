using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PRN221_Project_ShopOnline.Models;
using Microsoft.EntityFrameworkCore;

namespace PRN221_Project_ShopOnline.DAO
{
    public class ShipDAO
    {
        private ElectronicShopPRN221Context context = new ElectronicShopPRN221Context();

        /*-------------Customer-------------*/
        public List<Ship> GetAllShips()
        {
            context = new ElectronicShopPRN221Context();

            List<Ship> ships = context.Ships.ToList();
            return ships;
        }
    }
}
