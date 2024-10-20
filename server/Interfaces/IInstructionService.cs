using server.Dtos;
using server.Models;

namespace server.Interfaces;

public interface IInstructionService
{
    Task<Instruction> Create(InstructionDto dto);
    Instruction Delete(Instruction instruction);
}