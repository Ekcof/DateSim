// TODO: ������ object ��������� �����-�� ������ ������� � ���� ���������� ��� �������� ������ ��� ����������� ������
public interface ISaveData 
{
    public SerializedNPCData Serialize();
    public void Deserialize(SerializedNPCData data);
}
