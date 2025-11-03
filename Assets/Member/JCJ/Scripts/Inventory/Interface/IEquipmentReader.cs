public interface IEquipmentReader
{
    // 특정 슬롯의 장비 가져오기
    EquipmentSO GetEquippedItem(int slotIndex);
    
    // 모든 장착 장비 배열 가져오기
    EquipmentSO[] GetAllEquippedItems();
    
    // 모든 장비의 공격력 보너스 합계
    int GetTotalAttackBonus();
    
    // 모든 장비의 방어력 보너스 합계
    int GetTotalDefenseBonus();
}