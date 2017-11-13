
using UnityEngine;


public class Camera : MonoBehaviour
{
    public const ushort C_MOVE_LEFT = 0;
    public const ushort C_MOVE_FOUNT = 1;
    public const ushort C_MOVE_RIGHT = 2;
    public const ushort C_MOVE_BACK = 3;

    private float m_speed;
    private GameObject m_focus_go;

    // 主摄像机初始化方法
    void Start()
    {
        m_speed = 0.5f;
        m_focus_go = GameObject.Find( "Main Camera Helper" );

        ResetFoucusByCamera();
    }
    
    void Update()
    {
        ControlEvent();
    }


    /// <summary>
    /// 2017年6月1日 22:50:59s
    /// 全局控制方法
    /// </summary>
    private void ControlEvent()
    {
        // 方向键控制摄像头移动
        if (Input.GetKey( "d" ))
        {
            MoveCamera(C_MOVE_RIGHT);
        }
        if (Input.GetKey( "a" ))
        {
            MoveCamera(C_MOVE_LEFT);
        }
        if (Input.GetKey( "w" ))
        {
            MoveCamera(C_MOVE_FOUNT);
        }
        if (Input.GetKey( "s" ))
        {
            MoveCamera(C_MOVE_BACK);
        }

        // 镜头放大
        if (Input.GetAxis( "Mouse ScrollWheel" ) > 0)
        {
            Vector3 movePoistion = new Vector3( 0, 0, m_speed * 5 );
            transform.Translate( movePoistion );
        }
        // 镜头缩小
        if (Input.GetAxis( "Mouse ScrollWheel" ) < 0)
        {
            Vector3 movePoistion = new Vector3( 0, 0, -m_speed * 5 );
            transform.Translate( movePoistion );
        }


        // 测试方法
        if (Input.GetKeyDown("t"))
        {
            
        }


        if (Input.GetKeyDown("q"))
        {
            //Quaternion rotation = gameObject.transform.rotation;
            //rotation.eulerAngles = new Vector3( 0.0f, 10.0f, 0.0f );
            //gameObject.transform.rotation = rotation;
            //gameObject.transform.SetPositionAndRotation(this.gameObject.transform.position, rotation);
        }
        if (Input.GetKeyDown("e"))
        {
            //ResetFoucusByCamera();

            //Vector3 position = m_focus_go.transform.position;
        }
        if (Input.GetKeyDown( "r" ))
        {
            //gameObject.transform.Rotate( new Vector3( 10f, 0, 0 ) );
        }
        if (Input.GetKeyDown( "f" ))
        {
            //gameObject.transform.Rotate( new Vector3( -10f, 0, 0 ) );
        }
        if (Input.GetKeyDown("n"))
        {
            Guy.CreateGuy(10f, 0f, 0f, 1000, 10, 10);
        }


        // 鼠标中键 控制镜头旋转的方法
        if (Input.GetMouseButton(2))
        {
            float screen_x = Input.GetAxis( "Mouse X" ) * 1f;
            float screen_y = Input.GetAxis( "Mouse Y" ) * 1f;

            RotationCamera(screen_x, screen_y );

        }
    }


    /// <summary>
    /// 2017-07-25 08:27:59
    /// 移动摄像机的方法，只支持前后左右移动
    /// </summary>
    public void MoveCamera(ushort move_type)
    {
        Vector3 camera_euler = transform.rotation.eulerAngles;

        switch (move_type)
        {
            case C_MOVE_LEFT:
                {
                    float move_x = m_speed * Mathf.Sin(camera_euler.y * Mathf.PI / 180f + 3f / 2f * Mathf.PI);
                    float move_z = m_speed * Mathf.Sin(camera_euler.y * Mathf.PI / 180f);

                    // 移动摄像头
                    transform.position = new Vector3(transform.position.x + move_x, transform.position.y, transform.position.z + move_z);
                    // 移动焦点
                    m_focus_go.transform.position = new Vector3(m_focus_go.transform.position.x + move_x, m_focus_go.transform.position.y, m_focus_go.transform.position.z + move_z);
                }
                break;

            case C_MOVE_FOUNT:
                {
                    float move_x = m_speed * Mathf.Sin(camera_euler.y * Mathf.PI / 180f);
                    float move_z = m_speed * Mathf.Sin(camera_euler.y * Mathf.PI / 180f + 1f / 2f * Mathf.PI);

                    // 移动摄像头
                    transform.position = new Vector3(transform.position.x + move_x, transform.position.y, transform.position.z + move_z);
                    // 移动焦点
                    m_focus_go.transform.position = new Vector3(m_focus_go.transform.position.x + move_x, m_focus_go.transform.position.y, m_focus_go.transform.position.z + move_z);
                }
                break;

            case C_MOVE_RIGHT:
                {
                    float move_x = m_speed * Mathf.Sin(camera_euler.y * Mathf.PI / 180f + 1f / 2f * Mathf.PI);
                    float move_z = m_speed * Mathf.Sin(camera_euler.y * Mathf.PI / 180f + Mathf.PI);

                    // 移动摄像头
                    transform.position = new Vector3(transform.position.x + move_x, transform.position.y, transform.position.z + move_z);
                    // 移动焦点
                    m_focus_go.transform.position = new Vector3(m_focus_go.transform.position.x + move_x, m_focus_go.transform.position.y, m_focus_go.transform.position.z + move_z);
                }
                break;

            case C_MOVE_BACK:
                {
                    float move_x = m_speed * Mathf.Sin(camera_euler.y * Mathf.PI / 180f + Mathf.PI);
                    float move_z = m_speed * Mathf.Sin(camera_euler.y * Mathf.PI / 180f + 3f / 2f * Mathf.PI);

                    // 移动摄像头
                    transform.position = new Vector3(transform.position.x + move_x, transform.position.y, transform.position.z + move_z);
                    // 移动焦点
                    m_focus_go.transform.position = new Vector3(m_focus_go.transform.position.x + move_x, m_focus_go.transform.position.y, m_focus_go.transform.position.z + move_z);
                }
                break;

            default:
                Debug.Log("错误的移动方向：" + move_type + "\n\r");
                break;
        }
    }


    /// <summary>
    /// 2017-08-21 00:45:11
    /// 通过摄像机，重置焦点 与 焦点和摄像机相关的参数
    /// </summary>
    private void ResetFoucusByCamera()
    {
        Vector3 eulerAngles = transform.rotation.eulerAngles;

        // 平面上镜头和焦点的相对距离
        float plat_focus_to_camera_len = Mathf.Tan( eulerAngles.x * Mathf.PI / 180 ) *  transform.position.y;
        float focus_z = transform.position.z + Mathf.Cos( eulerAngles.y * Mathf.PI / 180 ) * plat_focus_to_camera_len;
        float focus_x = transform.position.x + Mathf.Sin( eulerAngles.y * Mathf.PI / 180 ) * plat_focus_to_camera_len;

        m_focus_go.transform.position = new Vector3( focus_x, 0f, focus_z );
    }


    /// <summary>
    /// 2017-08-22 00:59:38
    /// 根据屏幕移动的距离来决定镜头旋转的方法
    /// </summary>
    /// <param name="screen_x"></param>
    /// <param name="screen_y"></param>
    private void RotationCamera(float screen_x, float screen_y)
    {
        Vector3 camera_euler = transform.rotation.eulerAngles;

        float move_x = screen_x * 4f;

        // 水平（绕Y轴）移动角度
        transform.RotateAround( m_focus_go.transform.position, Vector3.up, move_x );                        // 旋转摄像机
        m_focus_go.transform.RotateAround( m_focus_go.transform.position, Vector3.up, move_x );   // 旋转焦点

        // 前倾后仰
        Vector3 vec3 = new Vector3( Mathf.Cos( camera_euler.y * Mathf.PI / 180f ), 0f, Mathf.Sin( camera_euler.y * Mathf.PI / 180f + Mathf.PI ) );
        transform.RotateAround( m_focus_go.transform.position, vec3, -screen_y );
    }
}
