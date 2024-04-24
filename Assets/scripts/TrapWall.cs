using UnityEngine;

public class TrapWall : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool hasTipped = false; // ���ڼ��ǽ���Ƿ����㵹
    private bool canKill = true; // ǽ���Ƿ����ɱ�����

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

            // Ӧ��һ����ת��
            rb.AddTorque(direction * 50f, ForceMode2D.Impulse);

            // ���ǽ�����㵹
            hasTipped = true;

            // �����һ��ʱ������Ӧ
            Invoke("FreezeWall", 1.5f);
        }
    }

    void FreezeWall()
    {
        // ����ǽ����ת���λ��
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        canKill = false; // ǽ��̶����پ���ɱ����
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        // ȷ����ײ��������ң�����ǽ����Ȼ����ɱ�����
        if (collision.gameObject.CompareTag("Player") && canKill)
        {
            // ���Ի�ȡ��ҵ� Movement ���
            Movement movement = collision.gameObject.GetComponent<Movement>();

            // ����ȡ�� Movement ����Ƿ�ǿ�
            if (movement != null)
            {
                // ������ҵ���������
                movement.kill();
            }
            else
            {
                // ��ӡ����򾯸���Ϣ����������
                Debug.LogWarning("Movement component not found on the player object!");
            }
        }
    }

}
