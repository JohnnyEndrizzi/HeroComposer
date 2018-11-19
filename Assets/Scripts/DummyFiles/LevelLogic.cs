using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLogic : MonoBehaviour
{
    public enum stageGrade { A, B, C, D, F };
    public stageGrade currentGrade;
    public float numericalStageScore;
    public float maxNumericalStageScore;
    public bool levelPassed;

    public stageGrade calculateStageGrade(float score)
    {
        if (score >= (0.9 * maxNumericalStageScore))
        {
            levelPassed = true;
            return stageGrade.A;
        }
        else if (score >= (0.75 * maxNumericalStageScore))
        {
            levelPassed = true;
            return stageGrade.B;
        }
        else if (score >= (0.65 * maxNumericalStageScore))
        {
            levelPassed = true;
            return stageGrade.C;
        }
        else if (score >= (0.5 * maxNumericalStageScore))
        {
            levelPassed = true;
            return stageGrade.D;
        }
        else
        {
            levelPassed = false;
            return stageGrade.F;
        }
    }
}
