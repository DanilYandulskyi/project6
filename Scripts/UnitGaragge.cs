using System.Collections.Generic;

public class UnitGaragge
{
    private List<Unit> _units = new List<Unit>();
    private List<Place> _places = new List<Place>();

    private int _unitsMaxCount;

    public UnitGaragge()
    {
        _unitsMaxCount = _places.Count;

        for (int i = 0; i < _units.Count; i++)
        {
            Place place = GetFreePlace();

            _units[i].SetInitialPosition(place.transform.position);
            place.BecomeFull();
        }
    }

    public bool HasFreeUnits
    {
        get 
        {
            Unit unit;

            return TryGetUnit(out unit);
        }
    }

    public int UnitsMaxCount => _unitsMaxCount;
    public int UnitsCount => _units.Count;

    public void AddNewUnit(Unit unit)
    {
        Place place = GetFreePlace();

        if (place != null)
        {
            _units.Add(unit);

            place.BecomeFull();
        }
    }

    public Place GetFreePlace()
    {
        for (int i = 0; i < _places.Count; i++)
        {
            if (_places[i].IsFull == false)
            {
                return _places[i];
            }
        }

        return null;
    }
     
    public bool TryGetUnit(out Unit unit)
    {
        for (int i = 0; i < _units.Count; i++)
        {
            if (_units[i].IsStanding)
            {
                unit = _units[i];
                return true;
            }
        }

        unit = null;
        return false;
    }
}
