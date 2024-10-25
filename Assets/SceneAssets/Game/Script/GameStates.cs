using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStates
{
    public enum players
    {
        player = 1,
        enemy = -1
    }
    public enum gameresults
    {
        win,
        lose,
        special
    }
    public static class scenenames_test
    {
        public const string title_test         = "Title_test";
        public const string prologue_test      = "Prologue_test";
        public const string intro_test         = "Intro_test";
        public const string instruction_test   = "Instruction_test";
        public const string game_test          = "Game_test";
        public const string result_test        = "Result_test";
        public const string endingwin_test     = "Endings_test/EndingWin_test";
        public const string endinglose_test    = "Endings_test/EndingLose_test";
        public const string endingspecial_test = "Endings_test/EndingSpecials_test";
        public const string outro_test         = "Outro_test";
    }
    public static class scenenames
    {
        public const string title         = "Title";
        public const string prologue      = "Prologue";
        public const string intro         = "Intro";
        public const string instruction   = "Instruction";
        public const string game          = "Game";
        public const string result        = "Result";
        public const string endingwin     = "EndingWin";
        public const string endinglose    = "EndingLose";
        public const string endingspecial = "EndingSpecials";
        public const string outro         = "Outro";
    }
}