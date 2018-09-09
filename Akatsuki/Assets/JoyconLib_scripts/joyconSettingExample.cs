using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class joyconSettingExample : MonoBehaviour
{
	private static readonly Joycon.Button[] m_buttons =
		Enum.GetValues( typeof( Joycon.Button ) ) as Joycon.Button[];

	private List<Joycon>    m_joycons;
	private Joycon          m_joyconL;
	private Joycon          m_joyconR;
	private Joycon.Button?  m_pressedButtonL;
	private Joycon.Button?  m_pressedButtonR;


	int RotateFlag=-1;
	float mokuteki=0.0f;
	//
	public float shind_L_kyori = 50.0f;//
	public int shindL_type = 0;//
	public float shind_R_kyori = 50.0f;
	public int shindR_type = 0;

	// カーソル用ポインターUI
	public RectTransform pointerRT;
	// 壁は含まない正方形の1辺の長さ
	// UIマップの1辺(Rect)
	private float UIMapScale = 480f;
	// 現実マップの1辺(World)
	private float MapScale = 120f;
	// public GameObject pointer;
	public bool[] lights;

	public AudioSource ashioto;
	public float Speed;

	//
	float joyL_timer;
	float joyR_timer;
	bool joyR_leftright=false;
	bool joyR_updown=false;
	float joyR_pad_time=0;
	float joyR_pad_time_second=0;

	// Y,X,A入力時に、Light_set_3Light_set_3dをコールする為
	public Light_setting _ls;
	// UIのマップ左上のオブジェクト、基準点
	public Transform UILeftUP;
	// 相対座標の為のfloat変数
	private float diff_x;
	private float diff_y;
	// UI上のMAPの大きさ(WorldTransform)
	private float UI_MAPwidth = 127.75f;
	// WorldでのMAPの大きさ
	private float MAPwidth = 12;


	private void Start()
	{
		m_joycons = JoyconManager.Instance.j;

		if ( m_joycons == null || m_joycons.Count <= 0 ) return;

		m_joyconL = m_joycons.Find( c =>  c.isLeft );
		m_joyconR = m_joycons.Find( c => !c.isLeft );
	}

	private void Update()
	{
		m_pressedButtonL = null;
		m_pressedButtonR = null;

		/* PC操作用

		if(Input.GetKeyDown(KeyCode.RightArrow)) {
			Vector2 pos = pointerRT.anchoredPosition;
			if(pos.x >= 850) return;
			pos.x += 40;
			pointerRT.anchoredPosition = pos;
		}
		if(Input.GetKeyDown(KeyCode.LeftArrow)) {
			Vector2 pos = pointerRT.anchoredPosition;
			if(pos.x <= 490) return;
			pos.x -= 40;
			pointerRT.anchoredPosition = pos;
		}
		if(Input.GetKeyDown(KeyCode.UpArrow)) {
			Vector2 pos = pointerRT.anchoredPosition;
			if(pos.y >= -97) return;
			pos.y += 40;
			pointerRT.anchoredPosition = pos;
		}
		if(Input.GetKeyDown(KeyCode.DownArrow)) {
			Vector2 pos = pointerRT.anchoredPosition;
			if(pos.y <= -457) return;
			pos.y -= 40;
			pointerRT.anchoredPosition = pos;
		}

		if(Input.GetKeyDown(KeyCode.Space)) {
			setLamp_pc();
		}

		 PC操作用
		 */

		if ( m_joycons == null || m_joycons.Count <= 0 ) return;

		foreach ( var button in m_buttons )
		{
			if ( m_joyconL.GetButton( button ) )
			{
				m_pressedButtonL = button;
			}
			if ( m_joyconR.GetButton( button ) )
			{
				m_pressedButtonR = button;
			}
		}

/*		if ( Input.GetKeyDown( KeyCode.Z ) )
		{
			m_joyconL.SetRumble( 160, 320, 0.1f, 200 );
		}
		if ( Input.GetKeyDown( KeyCode.X ) )
		{
			m_joyconL.SetRumble( 320, 640, 0.1f, 200 );
		}

		if ( Input.GetKeyDown( KeyCode.P ) )
		{
			m_joyconR.SetRumble( 160, 320, 0.1f, 200 );
		}
*/

		if (ashioto.isPlaying == true && Mathf.Abs (m_joyconL.GetStick () [0]) < 0.1f && Mathf.Abs (m_joyconL.GetStick () [1]) < 0.1f) {
			ashioto.Stop ();
		}
		if (ashioto.isPlaying == false && (Mathf.Abs (m_joyconL.GetStick () [0]) > 0.1f || Mathf.Abs (m_joyconL.GetStick () [1]) > 0.1f)) {
			ashioto.Play ();
		}


		if (m_joyconL.GetStick ()[0] > 0.1f) {
			Debug.Log ("右");
			//this.transform.position += transform.right*0.1f*m_joyconL.GetStick ()[0];
			this.transform.position += transform.right*Speed*m_joyconL.GetStick ()[0];
			//new Vector3 (m_joyconL.GetStick ()[0]*0.1f, 0, 0);
		}
		if (m_joyconL.GetStick ()[0] < -0.1f) {
			Debug.Log ("左");
			this.transform.position +=transform.right*Speed*m_joyconL.GetStick ()[0];
		}


		if (m_joyconL.GetStick ()[1] > 0.1f) {
			Debug.Log ("前");
			this.transform.position += transform.forward*Speed*m_joyconL.GetStick ()[1];
		}
		if (m_joyconL.GetStick ()[1] < -0.1f) {
			Debug.Log ("後");
			this.transform.position += transform.forward*Speed*m_joyconL.GetStick ()[1];
		}

		if ( (m_joyconL.GetButtonDown( Joycon.Button.DPAD_RIGHT )||
			m_joyconR.GetButtonDown( Joycon.Button.SHOULDER_2 ))&&RotateFlag==-1)
		{
			// 右ボタンが押された
			//RotateFlag=1;
			//transform.Rotate (new Vector3 (0, 1, 0), 90);
			mokuteki+=90f;
			if (mokuteki >= 360f) {
				mokuteki = 0f;
			}
			RotateFlag = 1;
		}
		if ( (m_joyconL.GetButtonDown( Joycon.Button.DPAD_LEFT )||
			m_joyconL.GetButtonDown( Joycon.Button.SHOULDER_2 ))&&RotateFlag==-1)
		{
			// 右ボタンが押された
			//RotateFlag=1;
			//transform.Rotate (new Vector3 (0, 1, 0), 90);
			mokuteki-=90f;
			if (mokuteki <= -90f) {
				mokuteki = 270f;
			}
			RotateFlag = 2;
		}
		if ( m_joyconL.GetButtonDown( Joycon.Button.DPAD_DOWN )&&RotateFlag==-1)
		{
			// 右ボタンが押された
			//RotateFlag=1;
			//transform.Rotate (new Vector3 (0, 1, 0), 90);
			mokuteki+=180f;
			if (mokuteki >= 360f) {
				mokuteki -= 360f;
			}
			RotateFlag = 0;
		}


		/////JoyConR
		if ( m_joyconR.GetButtonDown( Joycon.Button.DPAD_RIGHT ))
		{
			// R右ボタンが押された(Y):赤
			// convertUI_to_World(1);
			setLamp(1);
		}

		if ( m_joyconR.GetButtonDown( Joycon.Button.DPAD_UP ))
		{
			// R上ボタンが押された(X):青
			// convertUI_to_World(2);
			setLamp(2);
		}

		if ( m_joyconR.GetButtonDown( Joycon.Button.DPAD_LEFT ))
		{
			// R左ボタンが押された(A):白
			// convertUI_to_World(0);
			setLamp(0);
		}

		joyR_pad_time++;
		joyR_pad_time_second++;
		if (m_joyconR.GetStick ()[0] > 0.2f&&joyR_pad_time>8f) {
			joyR_pad_time = 0f;
			Debug.Log ("右");
			joyR_leftright = true;
			// pointer.transform.position +=new Vector3 (m_joyconR.GetStick ()[0]*10f, 0, 0);
			Vector2 pos = pointerRT.anchoredPosition;
			if(pos.x >= 850) return;
			pos.x += 40;
			pointerRT.anchoredPosition = pos;
		}
		if (m_joyconR.GetStick ()[0] < -0.2f&&joyR_pad_time>8f) {
			joyR_pad_time = 0f;
			Debug.Log ("左");
			joyR_leftright = true;
			// pointer.transform.position +=new Vector3 (m_joyconR.GetStick ()[0]*10f, 0, 0);
			Vector2 pos = pointerRT.anchoredPosition;
			if(pos.x <= 490) return;
			pos.x -= 40;
			pointerRT.anchoredPosition = pos;
		}
		if (joyR_leftright == true && (m_joyconR.GetStick () [0] < 0.2f && m_joyconR.GetStick () [0] > -0.2f)) {
			joyR_leftright = false;
		}

		if (m_joyconR.GetStick ()[1] > 0.2f&&joyR_pad_time_second>8f) {
			joyR_pad_time_second = 0f;
			Debug.Log ("前");
			// pointer.transform.position +=new Vector3 (0,m_joyconR.GetStick ()[1]*10f, 0);
			Vector2 pos = pointerRT.anchoredPosition;
			if(pos.y >= -97) return;
			pos.y += 40;
			pointerRT.anchoredPosition = pos;
		}
		if (m_joyconR.GetStick () [1] < -0.2f&&joyR_pad_time_second>8f) {
			joyR_pad_time_second = 0f;
			Debug.Log ("後");
			// pointer.transform.position += new Vector3 (0,m_joyconR.GetStick () [1] * 10f, 0);
			Vector2 pos = pointerRT.anchoredPosition;
			if(pos.y <= -457) return;
			pos.y -= 40;
			pointerRT.anchoredPosition = pos;
		}

		if (RotateFlag >= 0) {
			PlayerRound (RotateFlag);
		}

		vibration_L (shindL_type,shind_L_kyori);
		vibration_R (shindR_type,shind_R_kyori);
	}

	private void PlayerRound(int ID){

		if (Mathf.Abs ((this.transform.eulerAngles.y - mokuteki)) < 1f) {
			RotateFlag = -1;
		} else {
			if (ID == 1) {
				transform.Rotate (new Vector3 (0, 1, 0), 5);
			} else if (ID == 2) {
				transform.Rotate (new Vector3 (0, 1, 0), -5);
			} else if (ID == 0) {
				transform.Rotate (new Vector3 (0, 1, 0), -10);
			}
		}

	}

	public void vibration_L(int Type,float kyori){
		joyL_timer += Time.deltaTime;
	//	Debug.Log (joyL_timer);
		float tuyosa;
		tuyosa = (50.0f - kyori) * 0.003f + 0.05f;
		if (tuyosa > 0.3f) {
			tuyosa = 0.3f;
		}
		//joyL_timer++;


		switch (Type) {

		case 0:
			if (joyL_timer < kyori * 0.02f) {
				m_joyconL.SetRumble (160, 320, tuyosa, 1);
			} else {
				if (joyL_timer >= kyori * 0.04f) {
					joyL_timer = 0;
				}
			}
			break;
		case 1:
			if (joyL_timer < kyori * 0.01f) {
				m_joyconL.SetRumble (160, 320, tuyosa, 1);
			} else if (kyori * 0.02f < joyL_timer && joyL_timer < kyori * 0.03f) {
				m_joyconL.SetRumble (160, 320, tuyosa * 0.75f, 1);

			} else {
				if (joyL_timer >= kyori * 0.06f) {
					joyL_timer = 0;
				}
			}
			break;

		case 2:
			if (joyL_timer < kyori * 0.01f) {
				m_joyconL.SetRumble (160, 320, tuyosa, 1);
			} else if (kyori * 0.02f < joyL_timer && joyL_timer < kyori * 0.03f) {
				m_joyconL.SetRumble (160, 320, tuyosa * 1.2f, 1);
			} else if (kyori * 0.04f < joyL_timer && joyL_timer < kyori * 0.05f) {
				m_joyconL.SetRumble (160, 320, tuyosa, 1);
			} else {
				if (joyL_timer >= kyori * 0.08f) {
					joyL_timer = 0;
				}
			}
			break;
		}
	}

	public void vibration_R(int Type,float kyori){
		joyR_timer += Time.deltaTime;
	//	Debug.Log (joyR_timer);
		float tuyosa;
		tuyosa = (50.0f - kyori) * 0.003f + 0.05f;
		if (tuyosa > 0.3f) {
			tuyosa = 0.3f;
		}
		//joyR_timer++;


		switch (Type) {

		case 0:
			if (joyR_timer < kyori * 0.02f) {
				m_joyconR.SetRumble (160, 320, tuyosa, 1);
			} else {
				if (joyR_timer >= kyori * 0.04f) {
					joyR_timer = 0;
				}
			}
			break;
		case 1:
			if (joyR_timer < kyori * 0.01f) {
				m_joyconR.SetRumble (160, 320, tuyosa, 1);
			} else if (kyori * 0.02f < joyR_timer && joyR_timer < kyori * 0.03f) {
				m_joyconR.SetRumble (160, 320, tuyosa * 0.75f, 1);

			} else {
				if (joyR_timer >= kyori * 0.06f) {
					joyR_timer = 0;
				}
			}
			break;

		case 2:
			if (joyR_timer < kyori * 0.01f) {
				m_joyconR.SetRumble (160, 320, tuyosa, 1);
			} else if (kyori * 0.02f < joyR_timer && joyR_timer < kyori * 0.03f) {
				m_joyconR.SetRumble (160, 320, tuyosa * 1.2f, 1);
			} else if (kyori * 0.04f < joyR_timer && joyR_timer < kyori * 0.05f) {
				m_joyconR.SetRumble (160, 320, tuyosa, 1);
			} else {
				if (joyR_timer >= kyori * 0.08f) {
					joyR_timer = 0;
				}
			}
			break;
		}
	}

	// ランプ設置する関数
	private void setLamp(int color) {
		Vector2 pos = pointerRT.anchoredPosition;
		// UIの左上のマップがRectTransform(450,-57)、この点を基準点とする
		pos.x -= 450f;
		pos.y -= -57f;
		// +5fと-5fはオフセット値
		_ls.Light_set_3d( (pos.x * MapScale) / UIMapScale + 5f, 5f, (pos.y * MapScale) / UIMapScale - 5f, color);
	}

	/* パソコン用のランプ設置関数
	private void setLamp_pc() {
		Vector2 pos = pointerRT.anchoredPosition;
		// UIの左上のマップがRectTransform(450,-57)、この点を基準点とする
		pos.x -= 450f;
		pos.y -= -57f;
		// +5fと-5fはオフセット値
		_ls.Light_set_3d( (pos.x * MapScale) / UIMapScale + 5f, 5f, (pos.y * MapScale) / UIMapScale - 5f, 1);
	}
	*/

	/*

	private void convertUI_to_World(int color) {
		// 現在のポインターの位置と、基準点とのX,Yの差を取る
		diff_x = pointer.transform.position.x - UILeftUP.position.x;
		diff_y = pointer.transform.position.y - UILeftUP.position.y;

		_ls.Light_set_3d( (diff_x * MAPwidth) / UI_MAPwidth, (diff_y * MAPwidth) / UI_MAPwidth, 0, color);
	}

	*/









	/*

	private void OnGUI()
	{
		var style = GUI.skin.GetStyle( "label" );
		style.fontSize = 24;

		if ( m_joycons == null || m_joycons.Count <= 0 )
		{
			GUILayout.Label( "Joy-Con が接続されていません" );
			return;
		}

		if ( !m_joycons.Any( c => c.isLeft ) )
		{
			GUILayout.Label( "Joy-Con (L) が接続されていません" );
			return;
		}

		if ( !m_joycons.Any( c => !c.isLeft ) )
		{
			GUILayout.Label( "Joy-Con (R) が接続されていません" );
			return;
		}

		GUILayout.BeginHorizontal( GUILayout.Width( 900 ) );

		foreach ( var joycon in m_joycons )
		{
			var isLeft      = joycon.isLeft;
			var name        = isLeft ? "Joy-Con (L)" : "Joy-Con (R)";
			var key         = isLeft ? "Z キー" : "X キー";
			var button      = isLeft ? m_pressedButtonL : m_pressedButtonR;
			var stick       = joycon.GetStick();
			var gyro        = joycon.GetGyro();
			var accel       = joycon.GetAccel();
			var orientation = joycon.GetVector();

			GUILayout.BeginVertical( GUILayout.Width( 400 ) );
			GUILayout.Label( name );
			GUILayout.Label( key + "：振動" );
			GUILayout.Label( "押されているボタン：" + button );
			GUILayout.Label( string.Format( "スティック：({0}, {1})", stick[ 0 ], stick[ 1 ] ) );
			GUILayout.Label( "ジャイロ：" + gyro );
			GUILayout.Label( "加速度：" + accel );
			GUILayout.Label( "傾き：" + orientation );
			GUILayout.EndVertical();
		}

		GUILayout.EndHorizontal();
	}
	*/


}
