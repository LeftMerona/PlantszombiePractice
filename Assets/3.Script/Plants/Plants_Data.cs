using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Plant_Type
{
    Support = 0,
    Shooter,
    Tanker,
    Bomb
}

public class Plants_Data
{
    public int id;
    public string _name;
    public int _image_name;
    public int _cost;
    public Plant_Type _e_type;
    public float _attack_time;
    public int _MaxHP;
    public int _CurrentHP;
    public float _RespawnTime;

}
