
using UnityEngine;


public class WorldCamera : MonoBehaviour {
    public const ushort MOVE_LEFT = 0;
    public const ushort MOVE_FOUNT = 1;
    public const ushort MOVE_RIGHT = 2;
    public const ushort MOVE_BACK = 3;

    /// <summary>
    /// 镜头允许的最低点
    /// </summary>
    private float CAMERA_FOCUS_LENGTH = 20f;
    /// <summary>
    /// 镜头允许的最高点
    /// </summary>
    private float CAMERA_MAX_HEIGHT = 200f;

    /// <summary>
    /// 按 WASD 时，镜头移动的速度（取决于镜头和地面的相对高度）
    /// </summary>
    private float speed;
    private GameObject focus;

    // 主摄像机初始化方法
    void Start() {
        speed = 0.5f;
        focus = GameObject.Find("Main Camera Helper");

        ResetFoucusByCamera();
    }

    void Update() {
        // 执行子线程中需要在主线程中执行的方法（UI修改）
        ThreadTool threadTool = ThreadTool.GetInstance();
        while (threadTool.MainThread_RunOnWorldSceneLambda()) { }

        ControlEvent();
    }


    /// <summary>
    /// 2017年6月1日 22:50:59s
    /// 攝像頭移動的方法
    /// </summary>
    private void ControlEvent() {
        // 方向键控制摄像头移动
        if (Input.GetKey("d")) {
            MoveCamera(MOVE_RIGHT);
        }
        if (Input.GetKey("a")) {
            MoveCamera(MOVE_LEFT);
        }
        if (Input.GetKey("w")) {
            MoveCamera(MOVE_FOUNT);
        }
        if (Input.GetKey("s")) {
            MoveCamera(MOVE_BACK);
        }

        // 镜头拉近
        if (Input.GetAxis("Mouse ScrollWheel") > 0) {
            
            if (transform.rotation.eulerAngles.x > 10 ) {
                Vector3 tmpVec3 = focus.transform.position - transform.position;
                float cameraFocusLength = tmpVec3.magnitude;

                if (cameraFocusLength > CAMERA_FOCUS_LENGTH) {
                    Vector3 movePoistion = new Vector3(0, 0, 10f);
                    transform.Translate(movePoistion);
                }
            }
        }
        // 镜头拉远
        if (Input.GetAxis("Mouse ScrollWheel") < 0) {
            if (this.transform.position.y < CAMERA_MAX_HEIGHT) {
                Vector3 movePoistion = new Vector3(0, 0, -10f);
                transform.Translate(movePoistion);
            }
        }


        // 鼠标中键 控制镜头旋转的方法
        if (Input.GetMouseButton(2)) {
            float screen_x = Input.GetAxis("Mouse X") * 1f;
            float screen_y = Input.GetAxis("Mouse Y") * 1f;

            RotationCamera(screen_x, screen_y);
        }
    }
    /// <summary>
    /// 2017-07-25 08:27:59
    /// 移动摄像机的方法，只支持前后左右移动
    /// </summary>
    public void MoveCamera(ushort move_type) {
        Vector3 camera_euler = transform.rotation.eulerAngles;

        switch (move_type) {
            case MOVE_LEFT: {
                    float move_x = speed * Mathf.Sin(camera_euler.y * Mathf.PI / 180f + 3f / 2f * Mathf.PI);
                    float move_z = speed * Mathf.Sin(camera_euler.y * Mathf.PI / 180f);

                    // 移动摄像头
                    transform.position = new Vector3(transform.position.x + move_x, transform.position.y, transform.position.z + move_z);
                    // 移动焦点
                    focus.transform.position = new Vector3(focus.transform.position.x + move_x, focus.transform.position.y, focus.transform.position.z + move_z);
                }
                break;

            case MOVE_FOUNT: {
                    float move_x = speed * Mathf.Sin(camera_euler.y * Mathf.PI / 180f);
                    float move_z = speed * Mathf.Sin(camera_euler.y * Mathf.PI / 180f + 1f / 2f * Mathf.PI);

                    // 移动摄像头
                    transform.position = new Vector3(transform.position.x + move_x, transform.position.y, transform.position.z + move_z);
                    // 移动焦点
                    focus.transform.position = new Vector3(focus.transform.position.x + move_x, focus.transform.position.y, focus.transform.position.z + move_z);
                }
                break;

            case MOVE_RIGHT: {
                    float move_x = speed * Mathf.Sin(camera_euler.y * Mathf.PI / 180f + 1f / 2f * Mathf.PI);
                    float move_z = speed * Mathf.Sin(camera_euler.y * Mathf.PI / 180f + Mathf.PI);

                    // 移动摄像头
                    transform.position = new Vector3(transform.position.x + move_x, transform.position.y, transform.position.z + move_z);
                    // 移动焦点
                    focus.transform.position = new Vector3(focus.transform.position.x + move_x, focus.transform.position.y, focus.transform.position.z + move_z);
                }
                break;

            case MOVE_BACK: {
                    float move_x = speed * Mathf.Sin(camera_euler.y * Mathf.PI / 180f + Mathf.PI);
                    float move_z = speed * Mathf.Sin(camera_euler.y * Mathf.PI / 180f + 3f / 2f * Mathf.PI);

                    // 移动摄像头
                    transform.position = new Vector3(transform.position.x + move_x, transform.position.y, transform.position.z + move_z);
                    // 移动焦点
                    focus.transform.position = new Vector3(focus.transform.position.x + move_x, focus.transform.position.y, focus.transform.position.z + move_z);
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
    private void ResetFoucusByCamera() {
        Vector3 eulerAngles = transform.rotation.eulerAngles;

        // 平面上镜头和焦点的相对距离
        float plat_focus_to_camera_len = Mathf.Tan(eulerAngles.x * Mathf.PI / 180) * transform.position.y;
        float focus_z = transform.position.z + Mathf.Cos(eulerAngles.y * Mathf.PI / 180) * plat_focus_to_camera_len;
        float focus_x = transform.position.x + Mathf.Sin(eulerAngles.y * Mathf.PI / 180) * plat_focus_to_camera_len;

        focus.transform.position = new Vector3(focus_x, 0f, focus_z);
    }
    /// <summary>
    /// 2017-08-22 00:59:38
    /// 根据屏幕移动的距离来决定镜头旋转的方法
    /// </summary>
    /// <param name="screen_x"></param>
    /// <param name="screen_y"></param>
    private void RotationCamera(float screen_x, float screen_y) {

        if (transform.rotation.eulerAngles.x - screen_y < 10 && screen_y >0) {
            screen_y = 0;
        }

        Vector3 camera_euler = transform.rotation.eulerAngles;

        float move_x = screen_x * 4f;

        // 水平（绕Y轴）移动角度
        transform.RotateAround(focus.transform.position, Vector3.up, move_x);                        // 旋转摄像机
        focus.transform.RotateAround(focus.transform.position, Vector3.up, move_x);   // 旋转焦点

        // 前倾后仰
        Vector3 vec3 = new Vector3(Mathf.Cos(camera_euler.y * Mathf.PI / 180f), 0f, Mathf.Sin(camera_euler.y * Mathf.PI / 180f + Mathf.PI));
        transform.RotateAround(focus.transform.position, vec3, -screen_y);
    }

    /// <summary>
    /// 限制 foucus 的移动范围
    /// </summary>
    /// <param name="width"></param>
    /// <param name="length"></param>
    public void LimitFoucusArrea(float width, float length) {

    }
}
