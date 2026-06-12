using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public static AnimationController Instance { get; private set; }
    private void Awake()
    {
        // Ensure there's only one instance of PanelManager
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); // Keep this object across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }
    private void AddRotation(Transform target, float speed, int direction)
    {
        if (target == null) return;

        RotateAnimation rotate = target.gameObject.AddComponent<RotateAnimation>();
        rotate.Initialize(speed, direction);
    }
    public void CreateArchiveAnimation(GameObject gameObject)
    {
        Transform transform = gameObject.transform.Find("Decoration1");

        Transform background4Transform = transform.Find("Background4");
        Transform background5Transform = transform.Find("Background5");
        Transform group1Transform = transform.Find("Group1");
        Transform group2Transform = transform.Find("Group2");
        Transform group3Transform = transform.Find("Group3");

        Transform group1Background1Transform = transform.Find("Group1/Background1");
        Transform group1Background3Transform = transform.Find("Group1/Background3");
        Transform group1Background4Transform = transform.Find("Group1/Background4");
        Transform group1Background6Transform = transform.Find("Group1/Background6");
        Transform group1Background7Transform = transform.Find("Group1/Background7");
        Transform group1Background9Transform = transform.Find("Group1/Background9");

        Transform group2Background1Transform = transform.Find("Group2/Background1");
        Transform group2Background3Transform = transform.Find("Group2/Background3");
        Transform group2Background4Transform = transform.Find("Group2/Background4");
        Transform group2Background6Transform = transform.Find("Group2/Background6");
        Transform group2Background7Transform = transform.Find("Group2/Background7");
        Transform group2Background9Transform = transform.Find("Group2/Background9");

        Transform group3Background1Transform = transform.Find("Group3/Background1");
        Transform group3Background3Transform = transform.Find("Group3/Background3");
        Transform group3Background4Transform = transform.Find("Group3/Background4");
        Transform group3Background6Transform = transform.Find("Group3/Background6");
        Transform group3Background7Transform = transform.Find("Group3/Background7");
        Transform group3Background9Transform = transform.Find("Group3/Background9");

        AddRotation(background4Transform, 20f, 1);
        AddRotation(background5Transform, 20f, -1);

        AddRotation(group1Transform, 60f, -1); // cùng chiều
        AddRotation(group2Transform, 40f, 1);  // ngược chiều
        AddRotation(group3Transform, 20f, -1); // cùng chiều

        AddRotation(group1Background1Transform, 60f, -1);
        AddRotation(group1Background3Transform, 60f, 1);
        AddRotation(group1Background4Transform, 60f, -1);
        AddRotation(group1Background6Transform, 60f, 1);
        AddRotation(group1Background7Transform, 60f, -1);
        AddRotation(group1Background9Transform, 60f, 1);

        AddRotation(group2Background1Transform, 40f, -1);
        AddRotation(group2Background3Transform, 40f, 1);
        AddRotation(group2Background4Transform, 40f, -1);
        AddRotation(group2Background6Transform, 40f, 1);
        AddRotation(group2Background7Transform, 40f, -1);
        AddRotation(group2Background9Transform, 40f, 1);

        AddRotation(group3Background1Transform, 20f, -1);
        AddRotation(group3Background3Transform, 20f, 1);
        AddRotation(group3Background4Transform, 20f, -1);
        AddRotation(group3Background6Transform, 20f, 1);
        AddRotation(group3Background7Transform, 20f, -1);
        AddRotation(group3Background9Transform, 20f, 1);
    }
    public void CreateHICAAnimation(GameObject gameObject)
    {
        Transform transform = gameObject.transform.Find("Decoration1");

        Transform background4Transform = transform.Find("Background4");
        Transform background5Transform = transform.Find("Background5");
        Transform group1Transform = transform.Find("Group1");
        Transform group2Transform = transform.Find("Group2");
        Transform group3Transform = transform.Find("Group3");

        Transform group1Background1Transform = transform.Find("Group1/Background1");
        Transform group1Background3Transform = transform.Find("Group1/Background3");
        Transform group1Background4Transform = transform.Find("Group1/Background4");
        Transform group1Background6Transform = transform.Find("Group1/Background6");
        Transform group1Background7Transform = transform.Find("Group1/Background7");
        Transform group1Background9Transform = transform.Find("Group1/Background9");

        Transform group2Background1Transform = transform.Find("Group2/Background1");
        Transform group2Background3Transform = transform.Find("Group2/Background3");
        Transform group2Background4Transform = transform.Find("Group2/Background4");
        Transform group2Background6Transform = transform.Find("Group2/Background6");
        Transform group2Background7Transform = transform.Find("Group2/Background7");
        Transform group2Background9Transform = transform.Find("Group2/Background9");

        Transform group3Background1Transform = transform.Find("Group3/Background1");
        Transform group3Background3Transform = transform.Find("Group3/Background3");
        Transform group3Background4Transform = transform.Find("Group3/Background4");
        Transform group3Background6Transform = transform.Find("Group3/Background6");
        Transform group3Background7Transform = transform.Find("Group3/Background7");
        Transform group3Background9Transform = transform.Find("Group3/Background9");

        AddRotation(background4Transform, 20f, 1);
        AddRotation(background5Transform, 20f, -1);

        AddRotation(group1Transform, 60f, -1); // cùng chiều
        AddRotation(group2Transform, 40f, 1);  // ngược chiều
        AddRotation(group3Transform, 20f, -1); // cùng chiều

        AddRotation(group1Background1Transform, 60f, -1);
        AddRotation(group1Background3Transform, 60f, 1);
        AddRotation(group1Background4Transform, 60f, -1);
        AddRotation(group1Background6Transform, 60f, 1);
        AddRotation(group1Background7Transform, 60f, -1);
        AddRotation(group1Background9Transform, 60f, 1);

        AddRotation(group2Background1Transform, 40f, -1);
        AddRotation(group2Background3Transform, 40f, 1);
        AddRotation(group2Background4Transform, 40f, -1);
        AddRotation(group2Background6Transform, 40f, 1);
        AddRotation(group2Background7Transform, 40f, -1);
        AddRotation(group2Background9Transform, 40f, 1);

        AddRotation(group3Background1Transform, 20f, -1);
        AddRotation(group3Background3Transform, 20f, 1);
        AddRotation(group3Background4Transform, 20f, -1);
        AddRotation(group3Background6Transform, 20f, 1);
        AddRotation(group3Background7Transform, 20f, -1);
        AddRotation(group3Background9Transform, 20f, 1);
    }
    public void CreateHICBAnimation(GameObject gameObject)
    {
        Transform transform = gameObject.transform.Find("Decoration1");

        Transform background4Transform = transform.Find("Background4");
        Transform background5Transform = transform.Find("Background5");
        Transform group1Transform = transform.Find("Group1");
        Transform group2Transform = transform.Find("Group2");
        Transform group3Transform = transform.Find("Group3");

        Transform group1Background1Transform = transform.Find("Group1/Background1");
        Transform group1Background3Transform = transform.Find("Group1/Background3");
        Transform group1Background4Transform = transform.Find("Group1/Background4");
        Transform group1Background6Transform = transform.Find("Group1/Background6");
        Transform group1Background7Transform = transform.Find("Group1/Background7");
        Transform group1Background9Transform = transform.Find("Group1/Background9");

        Transform group2Background1Transform = transform.Find("Group2/Background1");
        Transform group2Background3Transform = transform.Find("Group2/Background3");
        Transform group2Background4Transform = transform.Find("Group2/Background4");
        Transform group2Background6Transform = transform.Find("Group2/Background6");
        Transform group2Background7Transform = transform.Find("Group2/Background7");
        Transform group2Background9Transform = transform.Find("Group2/Background9");

        Transform group3Background1Transform = transform.Find("Group3/Background1");
        Transform group3Background3Transform = transform.Find("Group3/Background3");
        Transform group3Background4Transform = transform.Find("Group3/Background4");
        Transform group3Background6Transform = transform.Find("Group3/Background6");
        Transform group3Background7Transform = transform.Find("Group3/Background7");
        Transform group3Background9Transform = transform.Find("Group3/Background9");

        AddRotation(background4Transform, 20f, 1);
        AddRotation(background5Transform, 20f, -1);

        AddRotation(group1Transform, 60f, -1); // cùng chiều
        AddRotation(group2Transform, 40f, 1);  // ngược chiều
        AddRotation(group3Transform, 20f, -1); // cùng chiều

        AddRotation(group1Background1Transform, 60f, -1);
        AddRotation(group1Background3Transform, 60f, 1);
        AddRotation(group1Background4Transform, 60f, -1);
        AddRotation(group1Background6Transform, 60f, 1);
        AddRotation(group1Background7Transform, 60f, -1);
        AddRotation(group1Background9Transform, 60f, 1);

        AddRotation(group2Background1Transform, 40f, -1);
        AddRotation(group2Background3Transform, 40f, 1);
        AddRotation(group2Background4Transform, 40f, -1);
        AddRotation(group2Background6Transform, 40f, 1);
        AddRotation(group2Background7Transform, 40f, -1);
        AddRotation(group2Background9Transform, 40f, 1);

        AddRotation(group3Background1Transform, 20f, -1);
        AddRotation(group3Background3Transform, 20f, 1);
        AddRotation(group3Background4Transform, 20f, -1);
        AddRotation(group3Background6Transform, 20f, 1);
        AddRotation(group3Background7Transform, 20f, -1);
        AddRotation(group3Background9Transform, 20f, 1);
    }
    public void CreateHIDCAnimation(GameObject gameObject)
    {
        Transform transform = gameObject.transform.Find("Decoration1");

        Transform background4Transform = transform.Find("Background4");
        Transform background5Transform = transform.Find("Background5");
        Transform group1Transform = transform.Find("Group1");
        Transform group2Transform = transform.Find("Group2");
        Transform group3Transform = transform.Find("Group3");

        Transform group1Background1Transform = transform.Find("Group1/Background1");
        Transform group1Background3Transform = transform.Find("Group1/Background3");
        Transform group1Background4Transform = transform.Find("Group1/Background4");
        Transform group1Background6Transform = transform.Find("Group1/Background6");
        Transform group1Background7Transform = transform.Find("Group1/Background7");
        Transform group1Background9Transform = transform.Find("Group1/Background9");

        Transform group2Background1Transform = transform.Find("Group2/Background1");
        Transform group2Background3Transform = transform.Find("Group2/Background3");
        Transform group2Background4Transform = transform.Find("Group2/Background4");
        Transform group2Background6Transform = transform.Find("Group2/Background6");
        Transform group2Background7Transform = transform.Find("Group2/Background7");
        Transform group2Background9Transform = transform.Find("Group2/Background9");

        Transform group3Background1Transform = transform.Find("Group3/Background1");
        Transform group3Background3Transform = transform.Find("Group3/Background3");
        Transform group3Background4Transform = transform.Find("Group3/Background4");
        Transform group3Background6Transform = transform.Find("Group3/Background6");
        Transform group3Background7Transform = transform.Find("Group3/Background7");
        Transform group3Background9Transform = transform.Find("Group3/Background9");

        AddRotation(background4Transform, 20f, 1);
        AddRotation(background5Transform, 20f, -1);

        AddRotation(group1Transform, 60f, -1); // cùng chiều
        AddRotation(group2Transform, 40f, 1);  // ngược chiều
        AddRotation(group3Transform, 20f, -1); // cùng chiều

        AddRotation(group1Background1Transform, 60f, -1);
        AddRotation(group1Background3Transform, 60f, 1);
        AddRotation(group1Background4Transform, 60f, -1);
        AddRotation(group1Background6Transform, 60f, 1);
        AddRotation(group1Background7Transform, 60f, -1);
        AddRotation(group1Background9Transform, 60f, 1);

        AddRotation(group2Background1Transform, 40f, -1);
        AddRotation(group2Background3Transform, 40f, 1);
        AddRotation(group2Background4Transform, 40f, -1);
        AddRotation(group2Background6Transform, 40f, 1);
        AddRotation(group2Background7Transform, 40f, -1);
        AddRotation(group2Background9Transform, 40f, 1);

        AddRotation(group3Background1Transform, 20f, -1);
        AddRotation(group3Background3Transform, 20f, 1);
        AddRotation(group3Background4Transform, 20f, -1);
        AddRotation(group3Background6Transform, 20f, 1);
        AddRotation(group3Background7Transform, 20f, -1);
        AddRotation(group3Background9Transform, 20f, 1);
    }
    public void CreateHIENAnimation(GameObject gameObject)
    {
        Transform transform = gameObject.transform.Find("Decoration1");

        Transform background4Transform = transform.Find("Background4");
        Transform background5Transform = transform.Find("Background5");
        Transform group1Transform = transform.Find("Group1");
        Transform group2Transform = transform.Find("Group2");
        Transform group3Transform = transform.Find("Group3");

        Transform group1Background1Transform = transform.Find("Group1/Background1");
        Transform group1Background3Transform = transform.Find("Group1/Background3");
        Transform group1Background4Transform = transform.Find("Group1/Background4");
        Transform group1Background6Transform = transform.Find("Group1/Background6");
        Transform group1Background7Transform = transform.Find("Group1/Background7");
        Transform group1Background9Transform = transform.Find("Group1/Background9");

        Transform group2Background1Transform = transform.Find("Group2/Background1");
        Transform group2Background3Transform = transform.Find("Group2/Background3");
        Transform group2Background4Transform = transform.Find("Group2/Background4");
        Transform group2Background6Transform = transform.Find("Group2/Background6");
        Transform group2Background7Transform = transform.Find("Group2/Background7");
        Transform group2Background9Transform = transform.Find("Group2/Background9");

        Transform group3Background1Transform = transform.Find("Group3/Background1");
        Transform group3Background3Transform = transform.Find("Group3/Background3");
        Transform group3Background4Transform = transform.Find("Group3/Background4");
        Transform group3Background6Transform = transform.Find("Group3/Background6");
        Transform group3Background7Transform = transform.Find("Group3/Background7");
        Transform group3Background9Transform = transform.Find("Group3/Background9");

        AddRotation(background4Transform, 20f, 1);
        AddRotation(background5Transform, 20f, -1);

        AddRotation(group1Transform, 60f, -1); // cùng chiều
        AddRotation(group2Transform, 40f, 1);  // ngược chiều
        AddRotation(group3Transform, 20f, -1); // cùng chiều

        AddRotation(group1Background1Transform, 60f, -1);
        AddRotation(group1Background3Transform, 60f, 1);
        AddRotation(group1Background4Transform, 60f, -1);
        AddRotation(group1Background6Transform, 60f, 1);
        AddRotation(group1Background7Transform, 60f, -1);
        AddRotation(group1Background9Transform, 60f, 1);

        AddRotation(group2Background1Transform, 40f, -1);
        AddRotation(group2Background3Transform, 40f, 1);
        AddRotation(group2Background4Transform, 40f, -1);
        AddRotation(group2Background6Transform, 40f, 1);
        AddRotation(group2Background7Transform, 40f, -1);
        AddRotation(group2Background9Transform, 40f, 1);

        AddRotation(group3Background1Transform, 20f, -1);
        AddRotation(group3Background3Transform, 20f, 1);
        AddRotation(group3Background4Transform, 20f, -1);
        AddRotation(group3Background6Transform, 20f, 1);
        AddRotation(group3Background7Transform, 20f, -1);
        AddRotation(group3Background9Transform, 20f, 1);
    }
    public void CreateHIHNAnimation(GameObject gameObject)
    {
        Transform transform = gameObject.transform.Find("Decoration1");

        Transform background4Transform = transform.Find("Background4");
        Transform background5Transform = transform.Find("Background5");
        Transform group1Transform = transform.Find("Group1");
        Transform group2Transform = transform.Find("Group2");
        Transform group3Transform = transform.Find("Group3");

        Transform group1Background1Transform = transform.Find("Group1/Background1");
        Transform group1Background3Transform = transform.Find("Group1/Background3");
        Transform group1Background4Transform = transform.Find("Group1/Background4");
        Transform group1Background6Transform = transform.Find("Group1/Background6");
        Transform group1Background7Transform = transform.Find("Group1/Background7");
        Transform group1Background9Transform = transform.Find("Group1/Background9");

        Transform group2Background1Transform = transform.Find("Group2/Background1");
        Transform group2Background3Transform = transform.Find("Group2/Background3");
        Transform group2Background4Transform = transform.Find("Group2/Background4");
        Transform group2Background6Transform = transform.Find("Group2/Background6");
        Transform group2Background7Transform = transform.Find("Group2/Background7");
        Transform group2Background9Transform = transform.Find("Group2/Background9");

        Transform group3Background1Transform = transform.Find("Group3/Background1");
        Transform group3Background3Transform = transform.Find("Group3/Background3");
        Transform group3Background4Transform = transform.Find("Group3/Background4");
        Transform group3Background6Transform = transform.Find("Group3/Background6");
        Transform group3Background7Transform = transform.Find("Group3/Background7");
        Transform group3Background9Transform = transform.Find("Group3/Background9");

        AddRotation(background4Transform, 20f, 1);
        AddRotation(background5Transform, 20f, -1);

        AddRotation(group1Transform, 60f, -1); // cùng chiều
        AddRotation(group2Transform, 40f, 1);  // ngược chiều
        AddRotation(group3Transform, 20f, -1); // cùng chiều

        AddRotation(group1Background1Transform, 60f, -1);
        AddRotation(group1Background3Transform, 60f, 1);
        AddRotation(group1Background4Transform, 60f, -1);
        AddRotation(group1Background6Transform, 60f, 1);
        AddRotation(group1Background7Transform, 60f, -1);
        AddRotation(group1Background9Transform, 60f, 1);

        AddRotation(group2Background1Transform, 40f, -1);
        AddRotation(group2Background3Transform, 40f, 1);
        AddRotation(group2Background4Transform, 40f, -1);
        AddRotation(group2Background6Transform, 40f, 1);
        AddRotation(group2Background7Transform, 40f, -1);
        AddRotation(group2Background9Transform, 40f, 1);

        AddRotation(group3Background1Transform, 20f, -1);
        AddRotation(group3Background3Transform, 20f, 1);
        AddRotation(group3Background4Transform, 20f, -1);
        AddRotation(group3Background6Transform, 20f, 1);
        AddRotation(group3Background7Transform, 20f, -1);
        AddRotation(group3Background9Transform, 20f, 1);
    }
    public void CreateHIINAnimation(GameObject gameObject)
    {
        Transform transform = gameObject.transform.Find("Decoration1");

        Transform background4Transform = transform.Find("Background4");
        Transform background5Transform = transform.Find("Background5");
        Transform group1Transform = transform.Find("Group1");
        Transform group2Transform = transform.Find("Group2");
        Transform group3Transform = transform.Find("Group3");

        Transform group1Background1Transform = transform.Find("Group1/Background1");
        Transform group1Background3Transform = transform.Find("Group1/Background3");
        Transform group1Background4Transform = transform.Find("Group1/Background4");
        Transform group1Background6Transform = transform.Find("Group1/Background6");
        Transform group1Background7Transform = transform.Find("Group1/Background7");
        Transform group1Background9Transform = transform.Find("Group1/Background9");

        Transform group2Background1Transform = transform.Find("Group2/Background1");
        Transform group2Background3Transform = transform.Find("Group2/Background3");
        Transform group2Background4Transform = transform.Find("Group2/Background4");
        Transform group2Background6Transform = transform.Find("Group2/Background6");
        Transform group2Background7Transform = transform.Find("Group2/Background7");
        Transform group2Background9Transform = transform.Find("Group2/Background9");

        Transform group3Background1Transform = transform.Find("Group3/Background1");
        Transform group3Background3Transform = transform.Find("Group3/Background3");
        Transform group3Background4Transform = transform.Find("Group3/Background4");
        Transform group3Background6Transform = transform.Find("Group3/Background6");
        Transform group3Background7Transform = transform.Find("Group3/Background7");
        Transform group3Background9Transform = transform.Find("Group3/Background9");

        AddRotation(background4Transform, 20f, 1);
        AddRotation(background5Transform, 20f, -1);

        AddRotation(group1Transform, 60f, -1); // cùng chiều
        AddRotation(group2Transform, 40f, 1);  // ngược chiều
        AddRotation(group3Transform, 20f, -1); // cùng chiều

        AddRotation(group1Background1Transform, 60f, -1);
        AddRotation(group1Background3Transform, 60f, 1);
        AddRotation(group1Background4Transform, 60f, -1);
        AddRotation(group1Background6Transform, 60f, 1);
        AddRotation(group1Background7Transform, 60f, -1);
        AddRotation(group1Background9Transform, 60f, 1);

        AddRotation(group2Background1Transform, 40f, -1);
        AddRotation(group2Background3Transform, 40f, 1);
        AddRotation(group2Background4Transform, 40f, -1);
        AddRotation(group2Background6Transform, 40f, 1);
        AddRotation(group2Background7Transform, 40f, -1);
        AddRotation(group2Background9Transform, 40f, 1);

        AddRotation(group3Background1Transform, 20f, -1);
        AddRotation(group3Background3Transform, 20f, 1);
        AddRotation(group3Background4Transform, 20f, -1);
        AddRotation(group3Background6Transform, 20f, 1);
        AddRotation(group3Background7Transform, 20f, -1);
        AddRotation(group3Background9Transform, 20f, 1);
    }
    public void CreateHIRNAnimation(GameObject gameObject)
    {
        Transform transform = gameObject.transform.Find("Decoration1");

        Transform background4Transform = transform.Find("Background4");
        Transform background5Transform = transform.Find("Background5");
        Transform group1Transform = transform.Find("Group1");
        Transform group2Transform = transform.Find("Group2");
        Transform group3Transform = transform.Find("Group3");

        Transform group1Background1Transform = transform.Find("Group1/Background1");
        Transform group1Background3Transform = transform.Find("Group1/Background3");
        Transform group1Background4Transform = transform.Find("Group1/Background4");
        Transform group1Background6Transform = transform.Find("Group1/Background6");
        Transform group1Background7Transform = transform.Find("Group1/Background7");
        Transform group1Background9Transform = transform.Find("Group1/Background9");

        Transform group2Background1Transform = transform.Find("Group2/Background1");
        Transform group2Background3Transform = transform.Find("Group2/Background3");
        Transform group2Background4Transform = transform.Find("Group2/Background4");
        Transform group2Background6Transform = transform.Find("Group2/Background6");
        Transform group2Background7Transform = transform.Find("Group2/Background7");
        Transform group2Background9Transform = transform.Find("Group2/Background9");

        Transform group3Background1Transform = transform.Find("Group3/Background1");
        Transform group3Background3Transform = transform.Find("Group3/Background3");
        Transform group3Background4Transform = transform.Find("Group3/Background4");
        Transform group3Background6Transform = transform.Find("Group3/Background6");
        Transform group3Background7Transform = transform.Find("Group3/Background7");
        Transform group3Background9Transform = transform.Find("Group3/Background9");

        AddRotation(background4Transform, 20f, 1);
        AddRotation(background5Transform, 20f, -1);

        AddRotation(group1Transform, 60f, -1); // cùng chiều
        AddRotation(group2Transform, 40f, 1);  // ngược chiều
        AddRotation(group3Transform, 20f, -1); // cùng chiều

        AddRotation(group1Background1Transform, 60f, -1);
        AddRotation(group1Background3Transform, 60f, 1);
        AddRotation(group1Background4Transform, 60f, -1);
        AddRotation(group1Background6Transform, 60f, 1);
        AddRotation(group1Background7Transform, 60f, -1);
        AddRotation(group1Background9Transform, 60f, 1);

        AddRotation(group2Background1Transform, 40f, -1);
        AddRotation(group2Background3Transform, 40f, 1);
        AddRotation(group2Background4Transform, 40f, -1);
        AddRotation(group2Background6Transform, 40f, 1);
        AddRotation(group2Background7Transform, 40f, -1);
        AddRotation(group2Background9Transform, 40f, 1);

        AddRotation(group3Background1Transform, 20f, -1);
        AddRotation(group3Background3Transform, 20f, 1);
        AddRotation(group3Background4Transform, 20f, -1);
        AddRotation(group3Background6Transform, 20f, 1);
        AddRotation(group3Background7Transform, 20f, -1);
        AddRotation(group3Background9Transform, 20f, 1);
    }
    public void CreateHISNAnimation(GameObject gameObject)
    {
        Transform transform = gameObject.transform.Find("Decoration1");

        Transform background4Transform = transform.Find("Background4");
        Transform background5Transform = transform.Find("Background5");
        Transform group1Transform = transform.Find("Group1");
        Transform group2Transform = transform.Find("Group2");
        Transform group3Transform = transform.Find("Group3");

        Transform group1Background1Transform = transform.Find("Group1/Background1");
        Transform group1Background3Transform = transform.Find("Group1/Background3");
        Transform group1Background4Transform = transform.Find("Group1/Background4");
        Transform group1Background6Transform = transform.Find("Group1/Background6");
        Transform group1Background7Transform = transform.Find("Group1/Background7");
        Transform group1Background9Transform = transform.Find("Group1/Background9");

        Transform group2Background1Transform = transform.Find("Group2/Background1");
        Transform group2Background3Transform = transform.Find("Group2/Background3");
        Transform group2Background4Transform = transform.Find("Group2/Background4");
        Transform group2Background6Transform = transform.Find("Group2/Background6");
        Transform group2Background7Transform = transform.Find("Group2/Background7");
        Transform group2Background9Transform = transform.Find("Group2/Background9");

        Transform group3Background1Transform = transform.Find("Group3/Background1");
        Transform group3Background3Transform = transform.Find("Group3/Background3");
        Transform group3Background4Transform = transform.Find("Group3/Background4");
        Transform group3Background6Transform = transform.Find("Group3/Background6");
        Transform group3Background7Transform = transform.Find("Group3/Background7");
        Transform group3Background9Transform = transform.Find("Group3/Background9");

        AddRotation(background4Transform, 20f, 1);
        AddRotation(background5Transform, 20f, -1);

        AddRotation(group1Transform, 60f, -1); // cùng chiều
        AddRotation(group2Transform, 40f, 1);  // ngược chiều
        AddRotation(group3Transform, 20f, -1); // cùng chiều

        AddRotation(group1Background1Transform, 60f, -1);
        AddRotation(group1Background3Transform, 60f, 1);
        AddRotation(group1Background4Transform, 60f, -1);
        AddRotation(group1Background6Transform, 60f, 1);
        AddRotation(group1Background7Transform, 60f, -1);
        AddRotation(group1Background9Transform, 60f, 1);

        AddRotation(group2Background1Transform, 40f, -1);
        AddRotation(group2Background3Transform, 40f, 1);
        AddRotation(group2Background4Transform, 40f, -1);
        AddRotation(group2Background6Transform, 40f, 1);
        AddRotation(group2Background7Transform, 40f, -1);
        AddRotation(group2Background9Transform, 40f, 1);

        AddRotation(group3Background1Transform, 20f, -1);
        AddRotation(group3Background3Transform, 20f, 1);
        AddRotation(group3Background4Transform, 20f, -1);
        AddRotation(group3Background6Transform, 20f, 1);
        AddRotation(group3Background7Transform, 20f, -1);
        AddRotation(group3Background9Transform, 20f, 1);
    }
    public void CreateHITNAnimation(GameObject gameObject)
    {
        Transform transform = gameObject.transform.Find("Decoration1");

        Transform background4Transform = transform.Find("Background4");
        Transform background5Transform = transform.Find("Background5");
        Transform group1Transform = transform.Find("Group1");
        Transform group2Transform = transform.Find("Group2");
        Transform group3Transform = transform.Find("Group3");

        Transform group1Background1Transform = transform.Find("Group1/Background1");
        Transform group1Background3Transform = transform.Find("Group1/Background3");
        Transform group1Background4Transform = transform.Find("Group1/Background4");
        Transform group1Background6Transform = transform.Find("Group1/Background6");
        Transform group1Background7Transform = transform.Find("Group1/Background7");
        Transform group1Background9Transform = transform.Find("Group1/Background9");

        Transform group2Background1Transform = transform.Find("Group2/Background1");
        Transform group2Background3Transform = transform.Find("Group2/Background3");
        Transform group2Background4Transform = transform.Find("Group2/Background4");
        Transform group2Background6Transform = transform.Find("Group2/Background6");
        Transform group2Background7Transform = transform.Find("Group2/Background7");
        Transform group2Background9Transform = transform.Find("Group2/Background9");

        Transform group3Background1Transform = transform.Find("Group3/Background1");
        Transform group3Background3Transform = transform.Find("Group3/Background3");
        Transform group3Background4Transform = transform.Find("Group3/Background4");
        Transform group3Background6Transform = transform.Find("Group3/Background6");
        Transform group3Background7Transform = transform.Find("Group3/Background7");
        Transform group3Background9Transform = transform.Find("Group3/Background9");

        AddRotation(background4Transform, 20f, 1);
        AddRotation(background5Transform, 20f, -1);

        AddRotation(group1Transform, 60f, -1); // cùng chiều
        AddRotation(group2Transform, 40f, 1);  // ngược chiều
        AddRotation(group3Transform, 20f, -1); // cùng chiều

        AddRotation(group1Background1Transform, 60f, -1);
        AddRotation(group1Background3Transform, 60f, 1);
        AddRotation(group1Background4Transform, 60f, -1);
        AddRotation(group1Background6Transform, 60f, 1);
        AddRotation(group1Background7Transform, 60f, -1);
        AddRotation(group1Background9Transform, 60f, 1);

        AddRotation(group2Background1Transform, 40f, -1);
        AddRotation(group2Background3Transform, 40f, 1);
        AddRotation(group2Background4Transform, 40f, -1);
        AddRotation(group2Background6Transform, 40f, 1);
        AddRotation(group2Background7Transform, 40f, -1);
        AddRotation(group2Background9Transform, 40f, 1);

        AddRotation(group3Background1Transform, 20f, -1);
        AddRotation(group3Background3Transform, 20f, 1);
        AddRotation(group3Background4Transform, 20f, -1);
        AddRotation(group3Background6Transform, 20f, 1);
        AddRotation(group3Background7Transform, 20f, -1);
        AddRotation(group3Background9Transform, 20f, 1);
    }
    public void CreateScienceFictionAnimation(GameObject gameObject)
    {
        Transform transform = gameObject.transform.Find("Decoration1");

        Transform background4Transform = transform.Find("Background4");
        Transform background5Transform = transform.Find("Background5");
        Transform group1Transform = transform.Find("Group1");
        Transform group2Transform = transform.Find("Group2");
        Transform group3Transform = transform.Find("Group3");

        Transform group1Background1Transform = transform.Find("Group1/Background1");
        Transform group1Background3Transform = transform.Find("Group1/Background3");
        Transform group1Background4Transform = transform.Find("Group1/Background4");
        Transform group1Background6Transform = transform.Find("Group1/Background6");
        Transform group1Background7Transform = transform.Find("Group1/Background7");
        Transform group1Background9Transform = transform.Find("Group1/Background9");

        Transform group2Background1Transform = transform.Find("Group2/Background1");
        Transform group2Background3Transform = transform.Find("Group2/Background3");
        Transform group2Background4Transform = transform.Find("Group2/Background4");
        Transform group2Background6Transform = transform.Find("Group2/Background6");
        Transform group2Background7Transform = transform.Find("Group2/Background7");
        Transform group2Background9Transform = transform.Find("Group2/Background9");

        Transform group3Background1Transform = transform.Find("Group3/Background1");
        Transform group3Background3Transform = transform.Find("Group3/Background3");
        Transform group3Background4Transform = transform.Find("Group3/Background4");
        Transform group3Background6Transform = transform.Find("Group3/Background6");
        Transform group3Background7Transform = transform.Find("Group3/Background7");
        Transform group3Background9Transform = transform.Find("Group3/Background9");

        AddRotation(background4Transform, 20f, 1);
        AddRotation(background5Transform, 20f, -1);

        AddRotation(group1Transform, 60f, -1); // cùng chiều
        AddRotation(group2Transform, 40f, 1);  // ngược chiều
        AddRotation(group3Transform, 20f, -1); // cùng chiều

        AddRotation(group1Background1Transform, 60f, -1);
        AddRotation(group1Background3Transform, 60f, 1);
        AddRotation(group1Background4Transform, 60f, -1);
        AddRotation(group1Background6Transform, 60f, 1);
        AddRotation(group1Background7Transform, 60f, -1);
        AddRotation(group1Background9Transform, 60f, 1);

        AddRotation(group2Background1Transform, 40f, -1);
        AddRotation(group2Background3Transform, 40f, 1);
        AddRotation(group2Background4Transform, 40f, -1);
        AddRotation(group2Background6Transform, 40f, 1);
        AddRotation(group2Background7Transform, 40f, -1);
        AddRotation(group2Background9Transform, 40f, 1);

        AddRotation(group3Background1Transform, 20f, -1);
        AddRotation(group3Background3Transform, 20f, 1);
        AddRotation(group3Background4Transform, 20f, -1);
        AddRotation(group3Background6Transform, 20f, 1);
        AddRotation(group3Background7Transform, 20f, -1);
        AddRotation(group3Background9Transform, 20f, 1);
    }
    public void CreateResearchAnimation(GameObject gameObject)
    {
        Transform transform = gameObject.transform.Find("Decoration1");

        Transform background4Transform = transform.Find("Background4");
        Transform background5Transform = transform.Find("Background5");
        Transform group1Transform = transform.Find("Group1");
        Transform group2Transform = transform.Find("Group2");
        Transform group3Transform = transform.Find("Group3");

        Transform group1Background1Transform = transform.Find("Group1/Background1");
        Transform group1Background3Transform = transform.Find("Group1/Background3");
        Transform group1Background4Transform = transform.Find("Group1/Background4");
        Transform group1Background6Transform = transform.Find("Group1/Background6");
        Transform group1Background7Transform = transform.Find("Group1/Background7");
        Transform group1Background9Transform = transform.Find("Group1/Background9");

        Transform group2Background1Transform = transform.Find("Group2/Background1");
        Transform group2Background3Transform = transform.Find("Group2/Background3");
        Transform group2Background4Transform = transform.Find("Group2/Background4");
        Transform group2Background6Transform = transform.Find("Group2/Background6");
        Transform group2Background7Transform = transform.Find("Group2/Background7");
        Transform group2Background9Transform = transform.Find("Group2/Background9");

        Transform group3Background1Transform = transform.Find("Group3/Background1");
        Transform group3Background3Transform = transform.Find("Group3/Background3");
        Transform group3Background4Transform = transform.Find("Group3/Background4");
        Transform group3Background6Transform = transform.Find("Group3/Background6");
        Transform group3Background7Transform = transform.Find("Group3/Background7");
        Transform group3Background9Transform = transform.Find("Group3/Background9");

        AddRotation(background4Transform, 20f, 1);
        AddRotation(background5Transform, 20f, -1);

        AddRotation(group1Transform, 60f, -1); // cùng chiều
        AddRotation(group2Transform, 40f, 1);  // ngược chiều
        AddRotation(group3Transform, 20f, -1); // cùng chiều

        AddRotation(group1Background1Transform, 60f, -1);
        AddRotation(group1Background3Transform, 60f, 1);
        AddRotation(group1Background4Transform, 60f, -1);
        AddRotation(group1Background6Transform, 60f, 1);
        AddRotation(group1Background7Transform, 60f, -1);
        AddRotation(group1Background9Transform, 60f, 1);

        AddRotation(group2Background1Transform, 40f, -1);
        AddRotation(group2Background3Transform, 40f, 1);
        AddRotation(group2Background4Transform, 40f, -1);
        AddRotation(group2Background6Transform, 40f, 1);
        AddRotation(group2Background7Transform, 40f, -1);
        AddRotation(group2Background9Transform, 40f, 1);

        AddRotation(group3Background1Transform, 20f, -1);
        AddRotation(group3Background3Transform, 20f, 1);
        AddRotation(group3Background4Transform, 20f, -1);
        AddRotation(group3Background6Transform, 20f, 1);
        AddRotation(group3Background7Transform, 20f, -1);
        AddRotation(group3Background9Transform, 20f, 1);
    }
    public void CreateSSWNAnimation(GameObject gameObject)
    {
        Transform transform = gameObject.transform.Find("Decoration1");

        Transform background4Transform = transform.Find("Background4");
        Transform background5Transform = transform.Find("Background5");
        Transform group1Transform = transform.Find("Group1");
        Transform group2Transform = transform.Find("Group2");
        Transform group3Transform = transform.Find("Group3");

        Transform group1Background1Transform = transform.Find("Group1/Background1");
        Transform group1Background3Transform = transform.Find("Group1/Background3");
        Transform group1Background4Transform = transform.Find("Group1/Background4");
        Transform group1Background6Transform = transform.Find("Group1/Background6");
        Transform group1Background7Transform = transform.Find("Group1/Background7");
        Transform group1Background9Transform = transform.Find("Group1/Background9");

        Transform group2Background1Transform = transform.Find("Group2/Background1");
        Transform group2Background3Transform = transform.Find("Group2/Background3");
        Transform group2Background4Transform = transform.Find("Group2/Background4");
        Transform group2Background6Transform = transform.Find("Group2/Background6");
        Transform group2Background7Transform = transform.Find("Group2/Background7");
        Transform group2Background9Transform = transform.Find("Group2/Background9");

        Transform group3Background1Transform = transform.Find("Group3/Background1");
        Transform group3Background3Transform = transform.Find("Group3/Background3");
        Transform group3Background4Transform = transform.Find("Group3/Background4");
        Transform group3Background6Transform = transform.Find("Group3/Background6");
        Transform group3Background7Transform = transform.Find("Group3/Background7");
        Transform group3Background9Transform = transform.Find("Group3/Background9");

        AddRotation(background4Transform, 20f, 1);
        AddRotation(background5Transform, 20f, -1);

        AddRotation(group1Transform, 60f, -1); // cùng chiều
        AddRotation(group2Transform, 40f, 1);  // ngược chiều
        AddRotation(group3Transform, 20f, -1); // cùng chiều

        AddRotation(group1Background1Transform, 60f, -1);
        AddRotation(group1Background3Transform, 60f, 1);
        AddRotation(group1Background4Transform, 60f, -1);
        AddRotation(group1Background6Transform, 60f, 1);
        AddRotation(group1Background7Transform, 60f, -1);
        AddRotation(group1Background9Transform, 60f, 1);

        AddRotation(group2Background1Transform, 40f, -1);
        AddRotation(group2Background3Transform, 40f, 1);
        AddRotation(group2Background4Transform, 40f, -1);
        AddRotation(group2Background6Transform, 40f, 1);
        AddRotation(group2Background7Transform, 40f, -1);
        AddRotation(group2Background9Transform, 40f, 1);

        AddRotation(group3Background1Transform, 20f, -1);
        AddRotation(group3Background3Transform, 20f, 1);
        AddRotation(group3Background4Transform, 20f, -1);
        AddRotation(group3Background6Transform, 20f, 1);
        AddRotation(group3Background7Transform, 20f, -1);
        AddRotation(group3Background9Transform, 20f, 1);
    }
    public void CreateUniverseAnimation(GameObject gameObject)
    {
        Transform transform = gameObject.transform.Find("Decoration1");

        Transform background4Transform = transform.Find("Background4");
        Transform background5Transform = transform.Find("Background5");
        Transform group1Transform = transform.Find("Group1");
        Transform group2Transform = transform.Find("Group2");
        Transform group3Transform = transform.Find("Group3");

        Transform group1Background1Transform = transform.Find("Group1/Background1");
        Transform group1Background3Transform = transform.Find("Group1/Background3");
        Transform group1Background4Transform = transform.Find("Group1/Background4");
        Transform group1Background6Transform = transform.Find("Group1/Background6");
        Transform group1Background7Transform = transform.Find("Group1/Background7");
        Transform group1Background9Transform = transform.Find("Group1/Background9");

        Transform group2Background1Transform = transform.Find("Group2/Background1");
        Transform group2Background3Transform = transform.Find("Group2/Background3");
        Transform group2Background4Transform = transform.Find("Group2/Background4");
        Transform group2Background6Transform = transform.Find("Group2/Background6");
        Transform group2Background7Transform = transform.Find("Group2/Background7");
        Transform group2Background9Transform = transform.Find("Group2/Background9");

        Transform group3Background1Transform = transform.Find("Group3/Background1");
        Transform group3Background3Transform = transform.Find("Group3/Background3");
        Transform group3Background4Transform = transform.Find("Group3/Background4");
        Transform group3Background6Transform = transform.Find("Group3/Background6");
        Transform group3Background7Transform = transform.Find("Group3/Background7");
        Transform group3Background9Transform = transform.Find("Group3/Background9");

        AddRotation(background4Transform, 20f, 1);
        AddRotation(background5Transform, 20f, -1);

        AddRotation(group1Transform, 60f, -1); // cùng chiều
        AddRotation(group2Transform, 40f, 1);  // ngược chiều
        AddRotation(group3Transform, 20f, -1); // cùng chiều

        AddRotation(group1Background1Transform, 60f, -1);
        AddRotation(group1Background3Transform, 60f, 1);
        AddRotation(group1Background4Transform, 60f, -1);
        AddRotation(group1Background6Transform, 60f, 1);
        AddRotation(group1Background7Transform, 60f, -1);
        AddRotation(group1Background9Transform, 60f, 1);

        AddRotation(group2Background1Transform, 40f, -1);
        AddRotation(group2Background3Transform, 40f, 1);
        AddRotation(group2Background4Transform, 40f, -1);
        AddRotation(group2Background6Transform, 40f, 1);
        AddRotation(group2Background7Transform, 40f, -1);
        AddRotation(group2Background9Transform, 40f, 1);

        AddRotation(group3Background1Transform, 20f, -1);
        AddRotation(group3Background3Transform, 20f, 1);
        AddRotation(group3Background4Transform, 20f, -1);
        AddRotation(group3Background6Transform, 20f, 1);
        AddRotation(group3Background7Transform, 20f, -1);
        AddRotation(group3Background9Transform, 20f, 1);
    }
    public void CreateRankAnimation(GameObject gameObject)
    {
        Transform transform = gameObject.transform.Find("Decoration1");

        Transform background4Transform = transform.Find("Background4");
        Transform background5Transform = transform.Find("Background5");
        Transform group1Transform = transform.Find("Group1");
        Transform group2Transform = transform.Find("Group2");
        Transform group3Transform = transform.Find("Group3");

        Transform group1Background1Transform = transform.Find("Group1/Background1");
        Transform group1Background3Transform = transform.Find("Group1/Background3");
        Transform group1Background4Transform = transform.Find("Group1/Background4");
        Transform group1Background6Transform = transform.Find("Group1/Background6");
        Transform group1Background7Transform = transform.Find("Group1/Background7");
        Transform group1Background9Transform = transform.Find("Group1/Background9");

        Transform group2Background1Transform = transform.Find("Group2/Background1");
        Transform group2Background3Transform = transform.Find("Group2/Background3");
        Transform group2Background4Transform = transform.Find("Group2/Background4");
        Transform group2Background6Transform = transform.Find("Group2/Background6");
        Transform group2Background7Transform = transform.Find("Group2/Background7");
        Transform group2Background9Transform = transform.Find("Group2/Background9");

        Transform group3Background1Transform = transform.Find("Group3/Background1");
        Transform group3Background3Transform = transform.Find("Group3/Background3");
        Transform group3Background4Transform = transform.Find("Group3/Background4");
        Transform group3Background6Transform = transform.Find("Group3/Background6");
        Transform group3Background7Transform = transform.Find("Group3/Background7");
        Transform group3Background9Transform = transform.Find("Group3/Background9");

        AddRotation(background4Transform, 20f, 1);
        AddRotation(background5Transform, 20f, -1);

        AddRotation(group1Transform, 60f, -1); // cùng chiều
        AddRotation(group2Transform, 40f, 1);  // ngược chiều
        AddRotation(group3Transform, 20f, -1); // cùng chiều

        AddRotation(group1Background1Transform, 60f, -1);
        AddRotation(group1Background3Transform, 60f, 1);
        AddRotation(group1Background4Transform, 60f, -1);
        AddRotation(group1Background6Transform, 60f, 1);
        AddRotation(group1Background7Transform, 60f, -1);
        AddRotation(group1Background9Transform, 60f, 1);

        AddRotation(group2Background1Transform, 40f, -1);
        AddRotation(group2Background3Transform, 40f, 1);
        AddRotation(group2Background4Transform, 40f, -1);
        AddRotation(group2Background6Transform, 40f, 1);
        AddRotation(group2Background7Transform, 40f, -1);
        AddRotation(group2Background9Transform, 40f, 1);

        AddRotation(group3Background1Transform, 20f, -1);
        AddRotation(group3Background3Transform, 20f, 1);
        AddRotation(group3Background4Transform, 20f, -1);
        AddRotation(group3Background6Transform, 20f, 1);
        AddRotation(group3Background7Transform, 20f, -1);
        AddRotation(group3Background9Transform, 20f, 1);
    }
    public void CreateMasterAnimation(GameObject gameObject)
    {
        Transform transform = gameObject.transform.Find("Decoration1");

        Transform background4Transform = transform.Find("Background4");
        Transform background5Transform = transform.Find("Background5");
        Transform group1Transform = transform.Find("Group1");
        Transform group2Transform = transform.Find("Group2");
        Transform group3Transform = transform.Find("Group3");

        Transform group1Background1Transform = transform.Find("Group1/Background1");
        Transform group1Background3Transform = transform.Find("Group1/Background3");
        Transform group1Background4Transform = transform.Find("Group1/Background4");
        Transform group1Background6Transform = transform.Find("Group1/Background6");
        Transform group1Background7Transform = transform.Find("Group1/Background7");
        Transform group1Background9Transform = transform.Find("Group1/Background9");

        Transform group2Background1Transform = transform.Find("Group2/Background1");
        Transform group2Background3Transform = transform.Find("Group2/Background3");
        Transform group2Background4Transform = transform.Find("Group2/Background4");
        Transform group2Background6Transform = transform.Find("Group2/Background6");
        Transform group2Background7Transform = transform.Find("Group2/Background7");
        Transform group2Background9Transform = transform.Find("Group2/Background9");

        Transform group3Background1Transform = transform.Find("Group3/Background1");
        Transform group3Background3Transform = transform.Find("Group3/Background3");
        Transform group3Background4Transform = transform.Find("Group3/Background4");
        Transform group3Background6Transform = transform.Find("Group3/Background6");
        Transform group3Background7Transform = transform.Find("Group3/Background7");
        Transform group3Background9Transform = transform.Find("Group3/Background9");

        AddRotation(background4Transform, 20f, 1);
        AddRotation(background5Transform, 20f, -1);

        AddRotation(group1Transform, 60f, -1); // cùng chiều
        AddRotation(group2Transform, 40f, 1);  // ngược chiều
        AddRotation(group3Transform, 20f, -1); // cùng chiều

        AddRotation(group1Background1Transform, 60f, -1);
        AddRotation(group1Background3Transform, 60f, 1);
        AddRotation(group1Background4Transform, 60f, -1);
        AddRotation(group1Background6Transform, 60f, 1);
        AddRotation(group1Background7Transform, 60f, -1);
        AddRotation(group1Background9Transform, 60f, 1);

        AddRotation(group2Background1Transform, 40f, -1);
        AddRotation(group2Background3Transform, 40f, 1);
        AddRotation(group2Background4Transform, 40f, -1);
        AddRotation(group2Background6Transform, 40f, 1);
        AddRotation(group2Background7Transform, 40f, -1);
        AddRotation(group2Background9Transform, 40f, 1);

        AddRotation(group3Background1Transform, 20f, -1);
        AddRotation(group3Background3Transform, 20f, 1);
        AddRotation(group3Background4Transform, 20f, -1);
        AddRotation(group3Background6Transform, 20f, 1);
        AddRotation(group3Background7Transform, 20f, -1);
        AddRotation(group3Background9Transform, 20f, 1);
    }
}