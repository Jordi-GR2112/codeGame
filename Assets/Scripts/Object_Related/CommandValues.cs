using UnityEngine;
using TMPro;


public class CommandValues : MonoBehaviour {
    public int number;
    public string tipo;
    public Color color;
    public string comando;
	public int loopIndex = 0;
    int tmp = 0;

    private void Start()
    {
        InvokeRepeating("UpdateValues", .1f, .4f);
    }

    public void UpdateValues()
    {
        switch (tipo)
        {
            case "render":
                if (transform.GetComponentInChildren<TMP_Dropdown>())
                {
                    if(_GameMode.GM.Colores[transform.GetComponentInChildren<TMP_Dropdown>().value] != color)
                    {
                        color = _GameMode.GM.Colores[transform.GetComponentInChildren<TMP_Dropdown>().value];
                    }
                }
                break;

            case "movimiento":
                if (transform.GetComponentInChildren<TMP_InputField>())  //cambia el numero de pasos a moverse adelante, si no hay numero en el inputtext se ajusta a 0
                {
                    if (int.TryParse(transform.GetComponentInChildren<TMP_InputField>().text, out tmp)) //se hace la conversion de texto a int y se almacena el valor a una variable temporal
                    {
                        if (tmp != number) //Si tmp es diferente al numero que se tenia almacenado...
                        {
                            number = tmp; //el numero almacenado cambia a tmp... 
                        }
                    }
                    else //si no hay un valor en la input text
                    {
                        number = 0;
                    }
                }
                break;

            case "control":
                if(int.TryParse(transform.Find("InputLoop").GetComponent<TMP_InputField>().text, out tmp))
                {
                    if(tmp != loopIndex)
                    {
                        loopIndex = tmp;
                    }
                }
                else
                {
                    loopIndex = 0;
                }
                break;
        }
    }
}
