select TOP(10)

AquariumId as '水槽ID',
Temperature as '温度',
SWITCHOFFSET (CreatedAt, '+09:00') as '測定日時'
from
ChipAquariumMobile.WaterTemperatures
order by 
CreatedAt DESC