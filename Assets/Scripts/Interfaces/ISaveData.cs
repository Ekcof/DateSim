// TODO: ������ object ��������� �����-�� ������ ������� � ���� ���������� ��� �������� ������ ��� ����������� ������
public interface ISaveData 
{
    public SerializableData Serialize();
    public void Deserialize(SerializableData data);
}
