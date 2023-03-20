using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField timeLimitInputField, fruitQuantityInputField;
    [SerializeField] private Toggle fireCampSpawnToggle, directionVibrationToggle, atRangeVibrationToggle;

    [SerializeField] private int defaultTimeLimit, defaultFruitQuantity;
    [SerializeField] private bool defaultFireCampSpawn, defaultDirectionVibration, defaultAtRangeVibration;

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
            ResetToDefaultTimeLimit();
        }

        if (PlayerPrefs.HasKey(FRUITAMOUNTNEEDED))
        {
            fruitQuantityInputField.text = PlayerPrefs.GetInt(FRUITAMOUNTNEEDED).ToString();
        }
        else
        {
            ResetToDefaultFruitQuantity();
        }

        if (PlayerPrefs.HasKey(SPAWNATFIRECAMP))
        {
            fireCampSpawnToggle.isOn = PlayerPrefs.GetInt(SPAWNATFIRECAMP) == 1;
        }
        else
        {
            ResetToDefaultSpawnAtFireCamp();
        }

        if (PlayerPrefs.HasKey(DIRECTIONVIBRATION))
        {
            directionVibrationToggle.isOn = PlayerPrefs.GetInt(DIRECTIONVIBRATION) == 1;
        }
        else
        {
            ResetToDefaultDirectionVibration();
        }

        if (PlayerPrefs.HasKey(ATRANGEVIBRATION))
        {
            atRangeVibrationToggle.isOn = PlayerPrefs.GetInt(ATRANGEVIBRATION) == 1;
        }
        else
        {
            ResetToDefaultDirectionVibration();
        }
    }

    public void ResetToDefaultTimeLimit()
    {
        PlayerPrefs.SetInt(TIMELIMIT, defaultTimeLimit);
        timeLimitInputField.text = defaultTimeLimit.ToString();
    }

    public void ResetToDefaultFruitQuantity()
    {
        PlayerPrefs.SetInt(FRUITAMOUNTNEEDED, defaultFruitQuantity);
        fruitQuantityInputField.text = defaultFruitQuantity.ToString();
    }

    public void ResetToDefaultSpawnAtFireCamp()
    {
        PlayerPrefs.SetInt(SPAWNATFIRECAMP, defaultFireCampSpawn ? 1 : 0);
        fireCampSpawnToggle.isOn = defaultFireCampSpawn;
    }

    public void ResetToDefaultDirectionVibration()
    {
        PlayerPrefs.SetInt(DIRECTIONVIBRATION, defaultDirectionVibration ? 1 : 0);
        directionVibrationToggle.isOn = defaultDirectionVibration;
    }

    public void ResetToDefaultAtRangeVibration()
    {
        PlayerPrefs.SetInt(ATRANGEVIBRATION, defaultAtRangeVibration ? 1 : 0);
        atRangeVibrationToggle.isOn = defaultAtRangeVibration;
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
