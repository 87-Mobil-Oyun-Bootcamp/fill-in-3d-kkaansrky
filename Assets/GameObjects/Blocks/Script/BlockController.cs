using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockState
{
    Default = 0,
    Collected
}

public class BlockController : MonoBehaviour
{

    Color color;
    Vector3 deneme;
    bool ısHere = true;
    public BlockState BlockState
    {
        get
        {
            return blockState;
        }

        set
        {
            blockState = value;

            switch (blockState)
            {
                case BlockState.Default:

                    OnCreated?.Invoke(this);

                    break;
                case BlockState.Collected:

                    OnCollected?.Invoke(this);

                    OnCreated -= LevelManager.Instance.OnBlockCreated;
                    OnCollected -= LevelManager.Instance.OnBlockCollected;

                    break;
                default:
                    break;
            }
        }
    }

    public System.Action<BlockController> OnCreated { get; set; }
    public System.Action<BlockController> OnCollected { get; set; }

    BlockState blockState = BlockState.Default;

    private void Start()
    {

        BlockState = BlockState.Default;
        color = GetComponent<MeshRenderer>().material.color;
        deneme = transform.position;
        gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
    }

    private void OnEnable()
    {
        OnCreated += LevelManager.Instance.OnBlockCreated;
        OnCollected += LevelManager.Instance.OnBlockCollected;
    }
    public Color getColor(GameObject other)
    {
        return other.GetComponent<Renderer>().material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            if (other.gameObject)
            {
                BlockState = BlockState.Collected;
                GetComponent<Collider>().enabled = false;
                other.GetComponent<Collider>().enabled = false;
                other.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
                other.gameObject.transform.position = new Vector3(deneme.x, deneme.y + 0.10f, deneme.z);
                other.GetComponent<Renderer>().material.color = color;

                other.gameObject.layer = LayerMask.NameToLayer("aaaa");
                other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            }
        }
    }
}
