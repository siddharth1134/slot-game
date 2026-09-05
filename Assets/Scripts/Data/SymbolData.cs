
using UnityEngine;

[CreateAssetMenu(
    fileName = "NewSymbol",
    menuName = "Slot Game/Symbol Data"

)]

public class SymbolData : ScriptableObject
{
    [SerializeField] 
    private string id;

    [SerializeField]
    private string displayName;

    [SerializeField]
    private Sprite sprite;

    public string Id => id;
    public string DisplayName => displayName;
    public Sprite Sprite => sprite;

}