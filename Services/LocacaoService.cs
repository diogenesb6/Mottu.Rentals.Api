using Mottu.Rentals.Api.Data;
using Mottu.Rentals.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mottu.Rentals.Api.Services
{
    public class LocacaoService
    {
        private readonly AppDbContext _context;

        public LocacaoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Locacao> CriarLocacaoAsync(Guid motoId, Guid entregadorId, int planoDias)
        {
            var moto = await _context.Motos.FindAsync(motoId)
                ?? throw new Exception("Moto não encontrada.");

            var entregador = await _context.Entregadores.FindAsync(entregadorId)
                ?? throw new Exception("Entregador não encontrado.");

            if (!entregador.TipoCNH.Contains("A"))
                throw new Exception("Somente entregadores com CNH categoria A podem alugar.");

            decimal valorDiaria = planoDias switch
            {
                7 => 30m,
                15 => 28m,
                30 => 22m,
                45 => 20m,
                50 => 18m,
                _ => throw new Exception("Plano inválido.")
            };

            var locacao = new Locacao
            {
                Id = Guid.NewGuid(),
                MotoId = motoId,
                EntregadorId = entregadorId,
                PlanoDias = planoDias,
                ValorDiaria = valorDiaria,
                DataInicio = DateTime.UtcNow.Date.AddDays(1),
                DataPrevistaTermino = DateTime.UtcNow.Date.AddDays(planoDias),
                ValorPrevisto = planoDias * valorDiaria,
                Ativa = true
            };

            _context.Locacoes.Add(locacao);
            await _context.SaveChangesAsync();

            return locacao;
        }

        public async Task<Locacao> FinalizarLocacaoAsync(Guid locacaoId, DateTime dataDevolucao)
        {
            var locacao = await _context.Locacoes.FindAsync(locacaoId)
                ?? throw new Exception("Locação não encontrada.");

            if (!locacao.Ativa)
                throw new Exception("Locação já finalizada.");

            locacao.ValorFinal = CalcularValorFinal(locacao, dataDevolucao);
            locacao.Ativa = false;

            await _context.SaveChangesAsync();
            return locacao;
        }

        private decimal CalcularValorFinal(Locacao locacao, DateTime dataDevolucao)
        {
            if (dataDevolucao < locacao.DataPrevistaTermino)
            {
                int diasUsados = Math.Max((dataDevolucao - locacao.DataInicio).Days, 1);
                int diasNaoUsados = locacao.PlanoDias - diasUsados;
                decimal valorBase = diasUsados * locacao.ValorDiaria;
                decimal multa = locacao.PlanoDias switch
                {
                    7 => diasNaoUsados * locacao.ValorDiaria * 0.20m,
                    15 => diasNaoUsados * locacao.ValorDiaria * 0.40m,
                    _ => 0m
                };
                return valorBase + multa;
            }

            if (dataDevolucao > locacao.DataPrevistaTermino)
            {
                int diasAtraso = (dataDevolucao - locacao.DataPrevistaTermino).Days;
                return locacao.ValorPrevisto + (diasAtraso * 50m);
            }

            return locacao.ValorPrevisto;
        }
    }
}
