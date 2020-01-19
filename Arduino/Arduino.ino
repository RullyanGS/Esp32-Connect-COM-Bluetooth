#include "BluetoothSerial.h"

#if !defined(CONFIG_BT_ENABLED) || !defined(CONFIG_BLUEDROID_ENABLED)
#error Bluetooth is not enabled! Please run `make menuconfig` to and enable it
#endif

BluetoothSerial SerialBT;

void setup()  
{      
  SerialBT.begin("ESP32");
  
  Serial.begin(9600);  
  pinMode(LED_BUILTIN, OUTPUT);  
}  

void loop()  
{  
  Serial.println("teste");
  SerialBT.println("teste");
  
  if(SerialBT.available())  
  {  
  char data = SerialBT.read();  
  SerialBT.println(data);  
  switch(data)  
  {  
    case 'O':digitalWrite(LED_BUILTIN, HIGH);  
    break;  
    case 'F':digitalWrite(LED_BUILTIN, LOW);  
    break;  
  }  
  }

  delay(100);
}  
