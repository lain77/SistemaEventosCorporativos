using System;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using SistemaEventosCorporativos.Core;
using SistemaEventosCorporativos.DATA;
using Microsoft.EntityFrameworkCore;

namespace SistemaEventosCorporativos.UI.UserControls
{
    public class EventosParticipanteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Participante participante)
            {
                using (var context = new AppDbContext())
                {
                    var eventos = context.ParticipanteEvento
                                         .Include(pe => pe.Evento)
                                         .Where(pe => pe.ParticipanteId == participante.Id)
                                         .Select(pe => pe.Evento.Nome)
                                         .ToList();

                    return string.Join(", ", eventos);
                }
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
