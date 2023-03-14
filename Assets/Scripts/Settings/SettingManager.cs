using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [SerializeField] TMP_InputField timeLimitInputField, fruitQuantityInputField;
    [SerializeField] Toggle fireCampSpawn, directionVibration, atRangeVibration;

    [SerializeField] int defaultTimeLimit, defaultFruitQuantity;
    [SerializeField] bool defaultFireCampSpawn, defaultDirectionVibration, defaultAtRangeVibration;

    const string TIMELIMIT = "TimeLimit";
    const string FRUITAMOUNTNEEDED = "FruitAmountNeeded";
    const string SPAWNATFIRECAMP = "SpawnAtTheFireCamp";
    const string DIRECTIONVIBRATION = "DirectionVibration";
    const string ATRANGEVIBRATION = "AtRangeVibration";

    public static int TimeLimit { get => PlayerPrefs.GetInt(TIMELIMIT); }
    public static int FruitAmountNeeded { get => PlayerPrefs.GetInt(FRUITAMOUNTNEEDED); }
    public static bool SpawnAtFireCamp { get => PlayerPrefs.GetInt(SPAWNATFIRECAMP) == 1; }
    public static bool DirectionVibration { get => PlayerPrefs.GetInt(DIRECTIONVIBRATION) == 1; }
    public static bool AtRangeVIbration { get => PlayerPrefs.GetInt(ATRANGEVIBRATION) == 1; }

    private void Start()
    {
        SetInitialValues();
    }

    private void SetInitialValues()
    {
        if (PlayerPrefs.HasKey(TIMELIMIT))
        {
            timeLimitInputField.text = PlayerPrefs.GetInt(TIMELIMIT).ToString();
        }
        else
        {
            PlayerPrefs.SetInt(TIMELIMIT, defaultTimeLimit);
        }

        if (PlayerPrefs.HasKey(FRUITAMOUNTNEEDED))
        {
            fruitQuantityInputField.text = PlayerPrefs.GetInt(FRUITAMOUNTNEEDED).ToString();
        }
        else
        {
            PlayerPrefs.SetInt(FRUITAMOUNTNEEDED, defaultFruitQuantity);
        }

        if (PlayerPrefs.HasKey(SPAWNATFIRECAMP))
        {
            fireCampSpawn.isOn = PlayerPrefs.GetInt(SPAWNATFIRECAMP) == 1;
        }
        else
        {
            PlayerPrefs.SetInt(SPAWNATFIRECAMP, defaultFireCampSpawn ? 1 : 0);
        }

        if (PlayerPrefs.HasKey(DIRECTIONVIBRATION))
        {
            directionVibration.isOn = PlayerPrefs.GetInt(DIRECTIONVIBRATION) == 1;
        }
        else
        {
            PlayerPrefs.SetInt(DIRECTIONVIBRATION, defaultDirectionVibration ? 1 : 0);
        }

        if (PlayerPrefs.HasKey(ATRANGEVIBRATION))
        {
            atRangeVibration.isOn = PlayerPrefs.GetInt(ATRANGEVIBRATION) == 1;
        }
        else
        {
            PlayerPrefs.SetInt(ATRANGEVIBRATION, defaultAtRangeVibration ? 1 : 0);
        }
    }

    public void ChangeTimeLimit(string newTimeLimit)
    {
        if (!int.TryParse(newTimeLimit, out int limit))
        {
            limit = 0;
            timeLimitInputField.text = "0";
        }
        PlayerPrefs.SetInt(TIMELIMIT, limit);
    }

    public void ChangeFruitQuantityNeeded(string newFruitQuantityNeeded)
    {
        if (!int.TryParse(newFruitQuantityNeeded, out int quantity))
        {
            quantity = 0;
            fruitQuantityInputField.text = "0";
        }
        PlayerPrefs.SetInt(FRUITAMOUNTNEEDED, quantity);
    }

    public void ChangeIfSpawningAtTheFireCamp(bool isSpawningAtTheFireCamp)
    {
        PlayerPrefs.SetInt(SPAWNATFIRECAMP, isSpawningAtTheFireCamp ? 1 : 0);
    }

    public void ChangeIfVibrationForDirection(bool isVibrationForDirection)
    {
        PlayerPrefs.SetInt(DIRECTIONVIBRATION, isVibrationForDirection ? 1 : 0);
    }

    public void ChangeIfVibrationForAtRange(bool isVibrationForAtRange)
    {
        PlayerPrefs.SetInt(ATRANGEVIBRATION, isVibrationForAtRange ? 1 : 0);
    }
}
