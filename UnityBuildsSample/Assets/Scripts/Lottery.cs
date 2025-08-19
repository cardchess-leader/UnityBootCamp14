using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;
using System;

public enum LotteryItemRarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}

[Serializable]
public class LotteryItem
{
    public string Name;
    public LotteryItemRarity Rarity;
}

public class Lottery : MonoBehaviour
{
    public List<LotteryItem> LotteryItems;
    public UnityEvent<LotteryItem> OnPickLottery;
    public Text text;

    public void RandomlyPickLottery()
    {
        int index = UnityEngine.Random.Range(0, LotteryItems.Count);
        LotteryItem pickedItem = LotteryItems[index];
        OnPickLottery?.Invoke(pickedItem);
    }

    public void ShowPickedLotteryItem(LotteryItem item)
    {
        string textColor;
        switch(item.Rarity)
        {
            case LotteryItemRarity.Common:
                textColor = "white";
                break;
            case LotteryItemRarity.Uncommon:
                textColor = "green";
                break;
            case LotteryItemRarity.Rare:
                textColor = "blue";
                break;
            case LotteryItemRarity.Epic:
                textColor = "purple";
                break;
            case LotteryItemRarity.Legendary:
                textColor = "orange";
                break;
            default:
                textColor = "white";
                break;
        }
        Debug.Log($"<color={textColor}>���� �޴�: {item.Name}, ���: {item.Rarity}</color>");
        if (item.Rarity >= LotteryItemRarity.Rare)
        {
            text.text = $"<color={textColor}>���� �޴�: {item.Name}\n" +
                $"���: {item.Rarity}</color>";
        } else
        {
            text.text = "���� �޴�: ";
        }
    }
}

// ��Ű��
// ����Ƽ�� ��Ű���� ������Ʈ�� �ʿ��� ���, ���̺귯��, ��, �ּµ��� ��Ƽ� �����ϴ� ���� ������ �ǹ��մϴ�.
// ��Ű�� ��� ���
// 1. Asset Store�� �ִ� �ּ��� �ٿ�޾Ƽ� ����Ѵ�.
// 2. Unity Registry(����Ƽ ������Ʈ��): ����Ƽ�� �����ϴ� ���� ��Ű�� ����ҷ�, �پ��� ��ɰ� ���� �����մϴ�. �߰����� ���� ��Ű���� Unity Package Manager�� ���� ��ġ�� �� �ֽ��ϴ�.

// ���� ����ϴ� ����Ƽ ���� ��Ű��
// 1. Cinemachine: ī�޶� ���� ����
// 2. TextMesh Pro: ��� �ؽ�Ʈ ������ ����
// 3. Input System: �Է� ó�� �ý���
// 4. Addressables: ���� ���� �� �ε� ����ȭ ����

// ��� ���: 
// Import: ����Ƽ �����Ϳ��� ��Ű���� �������� �۾� (����Ƽ �����Ϳ����� custom packages(������ ��Ű��)�� import�ϴ� ��ɰ�, �ּ� ������ �ٿ���� ����, ����Ƽ ������Ʈ�� �ʿ��� install�ϰ� import�ϴ� ������� �����ϴ�.)
// Export: ���� ������Ʈ�� �ִ� ������ ��Ű���� ����� �۾�

// ����Ƽ ��Ű���� ���� ����
// ��Ű���� ���� ��Ÿ������(.json)
// README.md (��Ű���� ���� ����)
// ���̼���
// ���� ���� ���
// ���� ���� (���̳� �ڵ�)
// ��Ÿ�� : ��ũ��Ʈ, ����
// ������ : ������ ���� �ڵ�
// �׽�Ʈ : �׽�Ʈ ���� �ڵ�

// ��Ű�� ���
// - ������Ʈ ���� ����
// - �ܺ� Git ��Ű��

// ��Ű���� ��� ����
// 1. ���� (Ŀ���� UI, JSON ������ �Ŵ���, ���� DB ����, ���� ��ũ��Ʈ)
// 2. �����Ϳ� ��Ÿ�� �ڵ��� �и� ����
// 3. ���� ���� �뵵�� ���� ������. -> Ư�� ������ ���� ����

// ����Ƽ ��Ʈ ��������
// Scene List�� ���ؼ� ���� ���� �ִ� ���� ����� ������ �� �ֽ��ϴ�.

// �÷��� ������ ������ �� �ֽ��ϴ�.

// �÷����� ���� ��ġ �۾��� �ʿ��մϴ�. (����Ƽ ��꿡���� Install)

// Player Settings�� ���� �ػ�, ������, �ΰ�, ���÷��� �̹���, ȸ���, ���� �� �� ���� ������Ʈ�� �ʿ��� �������� ������ �� �ֽ��ϴ�.

// Add Build Profile�� ���� �÷��� �� ������ ������ ����, ������ �� �ֽ��ϴ�.

// �߰��� �������Ͽ��� ������ �� �ִ� ������ ������ �����ϴ�.
// Override Global Scene List: ���� �� ����� ����� ����Դϴ�. �� ����� üũ�� ��� ������ ���� �������Ͽ��� ��ü������ ������ �� ��ϸ� ����� �� �ֽ��ϴ�.
// �� ������� �Ҹ��� �۾�? >> ���� ���� ���� (�ϼ��� �� �߿��� ���θ� ���Խ�Ű��, �ٸ� �� ����)

// >> Ư�� ��� �׽�Ʈ�� ���� ����
// >> ���� �ٸ� ���� ���� ������ ��Ȳ�� ���� ���嵵 ����

// Scripting Defines: ��ũ��Ʈ ���Ǹ� �߰��� �� �ֽ��ϴ�.
// C# �ڵ忡���� ��ó���� ���Ǹ� �����մϴ�.

// C# preprocessor(��ó����)
// �����Ͽ� �ڵ��� �Ϻθ� ���������� ���Խ�Ű�ų� ���ܽ�Ű�� �뵵�� ���Ǵ� ���� ó���Ǵ� ��ɵ��� �ǹ��մϴ�.

// # + ���ܾ� �������� �����˴ϴ�.
// ��ǥ������ ���Ǵ� C# ��ó���� ���þ�� ������ �����ϴ�:
// #define, #if, #else, #elif, #endif, #undef, #warning, #error
// �� ���þ��� ������ ������ �����ϴ�:
// #define: ��ó���� �ɺ��� �����մϴ�. �ɺ��� ���Ǻ� �������� ���� ���� ����Ƽ�� �±� ���� ���
// #define BUG_FIX
// #if: Ư�� �ɺ��� ���ǵǾ� �ִ��� Ȯ���ϰ�, �ش� ������ ���� ���� �ڵ带 ���Խ�ŵ�ϴ�.
// #if BUG_FIX
// #else: #if ������ ������ �� ������ �ڵ带 �����մϴ�.
// #elif: #if ������ ������ �� �ٸ� ������ Ȯ���մϴ�.
// #endif: #if �Ǵ� #elif�� ������ ���Ǻ� ������ ����� �����մϴ�.
// #undef: ���ǵ� �ɺ��� �����մϴ�.
// #warning: �����Ϸ����� ��� �޽����� ����մϴ�.
// #error: �����Ϸ����� ���� �޽����� ����ϰ� �������� �ߴ��մϴ�.
// #region, endregion: ��ũ��Ʈ �ڵ忡 ���� ���� ������ ����� �ݴϴ�.

// ����Ƽ������ Player Settings���� ������ �����ϰų� ����Ƽ 6 �̻��� ���������� Build Profile�� Scripting Defines�� �ۼ��մϴ�.

// 1. Project Settings -> Player - > Other Settings - > Scripting Define Symbols
// 2. Build Profile�� Add Build Profile -> Override Global Scene List üũ ��, Scripting Defines�� �ۼ�

// Player Settings���� �����ϴ� ������ ������ �����ϴ�:
// 1. CompanyName: ȸ�� �̸��� �����մϴ�.
// 2. ProductName: ������Ʈ/����/���� �̸��� �����մϴ�.
// 3. Version: ������Ʈ�� ������ �����մϴ�.

// Icon: �÷��� �� �������� �����ϴ� ����Դϴ�.
//       ����� �۾������� �ʼ������� �������ݴϴ�.
// Cursor: ���α׷� ���ο��� ����� Ŀ���� �̹��� -> �⺻ �̹����� ������ �� �ֽ��ϴ�.
// Resolution & Presentation ����
// Run In Background
// �ش� ������ ų ���, ����Ƽ ȭ�鿡�� ����� ����˴ϴ�.

// Standalone ���� ���� 
// ����Ƽ���� ������ �� �ִ� PC ȯ��� ��
// Windows, macOS, Linux

// 1. �÷��̾� �α� ���� ���� ����
// 2. â ũ�� ���� ��� (Resizable Window)
// 3. ���� â ��Ŀ���� �Ҿ ������������ ���� ����
// 4. alt + enter�� ��ü ȭ�� ���� ��ȯ���� ���� (Allow Fullscreen Switch)
// 5. Force Single Instance: ���� �ν��Ͻ� ���� �������� ���� (�� ���� �ϳ��� �ν��Ͻ��� ���� ����)
// 6. Use DGXI flip model swapchain for D3D11: DirectX 11���� �ø� �� ����ü���� ������� ���� (���� ��� ���ɼ� ����)

// Splash Image
// ���÷��� �̹��� ����
// �̹��� �ʿ�, �ΰ�: ȭ�鿡 ���̴� �ΰ� �̹���. ��׶���: ��� �̹���. �����Ϳ����� Preview�� ���� ��� üũ ����
// �ΰ� ��ɿ��� Show Unity Logo�� �����ϸ� 
// ��, �ΰ� ����Ʈ�� �ΰ� �߰��� �Ǿ����� ��

// �ΰ�� ��׶���� ����ϴ� �̹����� Sprite ������ ����� ��� �����մϴ�.
// �ΰ�� 2D UI �̹����� ���Ǹ�, ��׶���� 3D ������Ʈ�� ���˴ϴ�.