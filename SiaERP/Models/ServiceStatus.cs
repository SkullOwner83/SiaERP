﻿namespace SiaERP.Models
{
    public class ServiceStatus
    {
        //Service status fields
        private int _Id;
        private string _Name;

        //Service status properties
        public int Id { get => _Id; }
        public string Name { get => _Name; }

        //Constructor method
        public ServiceStatus(int id)
        {
            _Id = id;
            _Name = id switch
            {
                1 => "En servicio",
                2 => "Terminado",
                3 => "Entregado",
                _ => throw new System.NotImplementedException()
            };
        }
    }
}
