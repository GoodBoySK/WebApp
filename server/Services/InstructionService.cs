using server.Data;
using server.Dtos;
using server.Interfaces;
using server.Models;

namespace server.Services
{
    public class InstructionService(AppDbContext dbContext,IImageStorageService imageStorageService) : IInstructionService
    {
        public async Task<Instruction> Create(InstructionDto dto)
        {
            MediaFile? media = null;
            if (dto.Media?.Image != null)
            {
                media = await imageStorageService.StoreImageAsync(dto.Media.Image);
            }

            Instruction instruction = new Instruction
            {
                Description = dto.Description,
                Position = dto.Position,
                Media = media
            };
            var persisted = dbContext.Instructions.Add(instruction);
            return persisted.Entity;
        }

        public Instruction Delete(Instruction instruction)
        {
            if (instruction.Media is not null)
            {
                imageStorageService.DeleteImage(instruction.Media);
            }

            dbContext.Instructions.Remove(instruction);
            return instruction;
        }
    }
}
