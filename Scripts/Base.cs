using UnityEngine;
using System.Collections;

public class Base : MonoBehaviour
{
    [SerializeField] private Scanner _scanner;
    [SerializeField] private GoldDatabase _goldDatabase;
    [SerializeField] private Raycaster _raycaster;
    [SerializeField] private ClickProcessor _clickProcessor;
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private FlagHandler _flagHandler;
    [SerializeField] private GoldUIView _goldUIView;

    [SerializeField] private int _priceToSpawn;
    [SerializeField] private float _scanDelayTime = 1;

    private UnitGaragge _unitGaragge;
    private Warehouse _warehouse;

    private bool _isSelected;
    private WaitForSeconds _scanDelay;

    private void Start()
    {
        _unitGaragge = new UnitGaragge();
        _warehouse = new Warehouse(_goldUIView);
        _scanDelay = new WaitForSeconds(_scanDelayTime);

        StartCoroutine(Scan());
    }

    private void OnMouseDown()
    {
        _isSelected = !_isSelected;
    }

    private void SendUnitsToGold()
    {
        if (_unitGaragge.HasFreeUnits == false)
            return;

        var goldList = _scanner.Scan();

        goldList = _goldDatabase.GetFreeGold(goldList);

        if (goldList.Count == 0)
            return;

        foreach (var gold in goldList)
        {
            if (_unitGaragge.TryGetUnit(out Unit unit))
            {
                _goldDatabase.ReserveGold(gold);
                unit.SetGold(gold);
                unit.CollectedGold += CollectGold;
            }
            else
            {
                break;
            }
        }
    }

    private void OnDestroy()
    {
        _clickProcessor.Clicked -= SetFlag;
    }

    public void Initialize(Scanner scanner, UnitSpawner unitSpawner, FlagHandler flagHandler, Raycaster raycaster, ClickProcessor clickProcessor, GoldUIView goldUIView)
    {
        _scanner = scanner;
        _unitSpawner = unitSpawner;
        _flagHandler = flagHandler;
        _raycaster = raycaster;
        _clickProcessor = clickProcessor;
        _goldUIView = goldUIView;
    }

    public void Assign(Unit unit)
    {
        _unitGaragge.AddNewUnit(unit);
        
        unit.CollectedGold += CollectGold;
    }

    public void SetFlag()
    {
        if (_isSelected)
        {
            Vector3 flagSetPosition;

            if (_raycaster.Cast(Input.mousePosition, out flagSetPosition))
            {
                _flagHandler.SetFlag(new Vector3(flagSetPosition.x, flagSetPosition.y, flagSetPosition.z));
                _isSelected = false;
            }
        }
    }

    private void CollectGold(Gold gold, Unit unit)
    {
        if (_warehouse.TryAddGold(gold) && gold != null)
        {
            unit.CollectedGold -= CollectGold;

            if (_unitSpawner.UnitPrice <= _warehouse.GoldCount && (_unitGaragge.UnitsCount == 1 || _flagHandler.IsFlagSet == false) && _unitGaragge.UnitsCount <= _unitGaragge.UnitsMaxCount)
            {
                Unit spawnedUnit = _unitSpawner.SpawnUnit(_unitGaragge.GetFreePlace().transform.position);

                _unitGaragge.AddNewUnit(spawnedUnit);

                spawnedUnit.CollectedGold += CollectGold;

                _warehouse.DeleteGold(_unitSpawner.UnitPrice);
            }
        }
    }

    private IEnumerator Scan()
    {
        while (enabled)
        {
            yield return _scanDelay;

            SendUnitsToGold();
        }
    }
}