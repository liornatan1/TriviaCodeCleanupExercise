using System;

namespace Trivia;

public interface IGame
{
    bool Add(string playerName);

    void Roll(int roll);

    bool WasCorrectlyAnswered();

    bool WrongAnswer();
}