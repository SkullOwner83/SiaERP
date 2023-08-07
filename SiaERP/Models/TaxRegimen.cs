namespace SiaERP.Models
{
    public class TaxRegime
    {
        //Tax regimen fields
        private int _Id;
        private string _Name;

        //Tax regimen properties
        public int Id { get => _Id; }
        public string Name { get => _Name; }

        //Constructor method
        public TaxRegime(int id)
        {
            _Id = id;
            _Name = id switch
            {
                1 => "General de ley personas morales",
                2 => "Personas morales con fines no lucrativos",
                3 => "Sueldos y salarios e ingresos asimilados a salarios",
                4 => "Arrendamiento",
                5 => "Régimen de enajenación o adquisición de bienes",
                6 => "Demás ingresos",
                7 => "Residentes en el extranjero sin establecimiento permanente en México",
                8 => "Ingresos por dividendos (Socios y accionistas)",
                9 => "Personas físicas con actividades empresariales y profesionales",
                10 => "Ingresos por intereses",
                11 => "Régimen de los ingresos por obtención de premios",
                12 => "Sin obligaciones fiscales",
                13 => "Sociedades cooperativas de producción que optan por definir sus ingresos",
                14 => "Incorporación fiscal",
                15 => "Actividades agrícolas, ganaderas, silvícolas y pesqueras",
                16 => "Opcional para grupos de sociedades",
                17 => "Coordinados",
                18 => "Régimen de las actividades empresariales con ingresos a través de plataformas tecnológicas",
                19 => "Régimen simplificado de confianza",
                _ => throw new System.NotImplementedException()
            };
        }
    }
}
