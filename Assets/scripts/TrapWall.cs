using UnityEngine;

public class TrapWall : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool hasTipped = false; // 用于检测墙体是否已倾倒
    private bool canKill = true; // 墙体是否可以杀死玩家

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasTipped)
        {
            Vector2 contactPoint = collision.contacts[0].point;
            Vector2 center = rb.worldCenterOfMass;
            float direction = contactPoint.x > center.x ? -1f : 1f;

            // 应用一个旋转力
            rb.AddTorque(direction * 50f, ForceMode2D.Impulse);

            // 标记墙体已倾倒
            hasTipped = true;

            // 给玩家一点时间来反应
            Invoke("FreezeWall", 1.5f);
        }
    }

    void FreezeWall()
    {
        // 锁定墙体旋转后的位置
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        canKill = false; // 墙体固定后不再具有杀伤力
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        // 确保碰撞对象是玩家，并且墙体仍然可以杀死玩家
        if (collision.gameObject.CompareTag("Player") && canKill)
        {
            // 尝试获取玩家的 Movement 组件
            Movement movement = collision.gameObject.GetComponent<Movement>();

            // 检查获取的 Movement 组件是否非空
            if (movement != null)
            {
                // 调用玩家的死亡函数
                movement.kill();
            }
            else
            {
                // 打印错误或警告信息，帮助调试
                Debug.LogWarning("Movement component not found on the player object!");
            }
        }
    }

}
