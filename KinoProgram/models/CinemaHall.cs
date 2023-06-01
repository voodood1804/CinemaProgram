using KinoProgram.Application.models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoProgram.models
{
    [Table("CinemaHall")]
    public class CinemaHall : IEntity<int>
    {
        public CinemaHall(int rows, int columns) 
        {
            Rows = rows;
            Columns = columns;
            Guid = Guid.NewGuid();
        }

        public int Id { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public Guid Guid { get; private set; }

        public int HallSeats(int rows, int columns)
        {
            if(rows == 0 || columns == 0)
                return 0;

            return rows * columns;
        }
    }
}
