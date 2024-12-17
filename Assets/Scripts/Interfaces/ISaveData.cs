// TODO: Вместо object придумать какую-то другую обертку в виде интерфейса или базового класса для сохраняемых данных
public interface ISaveData 
{
    public SerializedNPCData Serialize();
    public void Deserialize(SerializedNPCData data);
}
