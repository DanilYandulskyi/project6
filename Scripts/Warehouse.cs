using System.Collections.Generic;

public class Warehouse
{
    private List<Gold> _goldList = new List<Gold>();
    private GoldUIView _goldUIView;
    
    public Warehouse(GoldUIView goldUIView)
    {
        _goldUIView = goldUIView;
    }

    public int GoldCount => _goldList.Count;
         
    public bool TryAddGold(Gold gold)
    {
        if (_goldList.Contains(gold) == false && gold != null)
        {
            _goldList.Add(gold);
            return true;
        }

        return false;
    }

    public void DeleteGold(int amount)
    {
        if (amount <= _goldList.Count)
        {
            _goldList.RemoveRange(0, amount);

            _goldUIView.UpdateText(_goldList.Count);
        }
    }
}
