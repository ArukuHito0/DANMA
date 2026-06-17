using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Weapon
{
    public Weapon(GameObject owner)
    {
        this.owner = owner;
    }

    private GameObject owner;
    
    private WeaponData weaponData;

    private readonly Dictionary<float, WaitForSeconds> waitTimeDict = new Dictionary<float, WaitForSeconds>();
    private WaitForSeconds GetWait(float time)
    {
        if(!waitTimeDict.ContainsKey(time))
            return waitTimeDict[time] = new WaitForSeconds(time);
        else
            return waitTimeDict[time];
    }

    public void SetWeaponData(WeaponData weaponData) => this.weaponData = weaponData;
    public WeaponData GetWeaponData() => this.weaponData;

    private Coroutine activeCoroutine;

    public void StartAttack(MonoBehaviour runner)
    {
        StopAttack(runner);
        activeCoroutine = runner.StartCoroutine(WeaponAttackCycle());
    }

    public void StopAttack(MonoBehaviour runner)
    {
        if (activeCoroutine != null)
        {
            runner.StopCoroutine(activeCoroutine);
            activeCoroutine = null;
        }
    }

    /// <summary>
    /// 武器の攻撃サイクルコルーチン
    /// </summary>
    /// <returns></returns>
    private IEnumerator WeaponAttackCycle()
    {
        while (true)
        {
            // クールタイムを開始
            yield return GetWait(weaponData.CoolTime);

            // 敵を狙う場合は射程内に敵がくるまで待機
            if (weaponData.isTargetting)
                yield return new WaitUntil(() => GetTarget.GetTargetInRange(EnemyBase.enemyList, owner.transform.position, weaponData.Range) != null);
            
            // 射撃コルーチン開始
            yield return Shooting();
        }
    }

    /// <summary>
    /// 射撃サイクル
    /// </summary>
    /// <returns></returns>
    private IEnumerator Shooting()
    {
        if (weaponData.bulletCnt <= 0) yield break;

        // 射撃方向を設定
        var targetAngle = AngleOfBase();
                
        for (int i = 0; i < weaponData.bulletCnt; i++)
        {
            // 弾の発射方向を取得
            float angle = AngleOfBullet(i);

            // 発射方向をラジアンに変換
            float rad = RadOfBullet(targetAngle, angle);

            // 正規化されたベクトルに弾を打ち出す
            ShotBullet(rad);

            if (weaponData.cycleTime != 0)
                yield return GetWait(weaponData.fireRate);
        }
    }

    /// <summary>
    /// 武器の発射角度を取得
    /// </summary>
    /// <returns></returns>
    private float AngleOfBase()
    {
        var target = GetTarget.GetTargetInRange(EnemyBase.enemyList, owner.transform.position, weaponData.Range);

        // 敵を狙わない場合、固定の発射角度を返す
        if(target == null || !weaponData.isTargetting) return weaponData.baseAngle;

        // 武器の使用者からのターゲット位置への角度を返す
        return Mathf.Atan2(
            target.transform.position.y - owner.transform.position.y,
            target.transform.position.x - owner.transform.position.x
            ) * Mathf.Rad2Deg;
    }

    /// <summary>
    /// 弾の発射角度を取得
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    private float AngleOfBullet(int num)
    {
        // 拡散角度が0または発射する弾数が１以下の場合、0度を返す
        if(weaponData.spreadAngle == 0 || weaponData.bulletCnt <= 1) return 0;

        // 拡散角度が全方位の場合、拡散角度を弾数でそのまま割った角度に弾の番号をかけ、その角度を返す
        // 拡散角度が全方位でない場合、拡散角度を弾数 -１で割り出た角度から、拡散角度の半分を引き、それに弾の番号をかけた角度を返す
        // ※扇形の真ん中を発射方向に持ってくるための計算
        if (weaponData.spreadAngle == 360.0f)
            return (weaponData.spreadAngle / (float)weaponData.bulletCnt) * num;
        else
            return -(weaponData.spreadAngle / 2f) + (weaponData.spreadAngle / (float)(weaponData.bulletCnt - 1)) * num;
    }

    /// <summary>
    /// 武器の発射角度と弾の発射角度を足した角度を返す
    /// </summary>
    /// <param name="shotAngle"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    private float AngleOfShot(float shotAngle, float angle)
    {
        return shotAngle + angle;
    }

    /// <summary>
    /// 弾の発射角度に射撃エラーの補正を加えた角度を返す
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    private float AngleOfError(float angle)
    {
        angle += Random.Range(-weaponData.dispersion, weaponData.dispersion) / 100f * Mathf.Rad2Deg;

        if(weaponData.spreadAngle != 360)
            return Mathf.Clamp(angle, -(weaponData.spreadAngle / 2f), (weaponData.spreadAngle / 2));
        else
            return angle;
    }

    /// <summary>
    /// 発射角度をラジアンに変換した正規化されたベクトルを返す
    /// </summary>
    /// <param name="targetAngle"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    private float RadOfBullet(float targetAngle, float angle)
    {
        if (weaponData.dispersion != 0)
            return AngleOfShot(targetAngle, AngleOfError(angle)) * Mathf.Deg2Rad;
        else
            return AngleOfShot(targetAngle, angle) * Mathf.Deg2Rad;
    }

    /// <summary>
    /// 弾の移動ベクトルを返す
    /// </summary>
    /// <param name="rad"></param>
    /// <returns></returns>
    private Vector2 BulletVelocity(float rad)
    {
        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * weaponData.bulletSpeed;
    }

    /// <summary>
    /// 弾を生成し、それに移動ベクトルを渡す
    /// </summary>
    /// <param name="rad"></param>
    private void ShotBullet(float rad)
    {
        ObjectPoolManager.Instance.GetPooledObject(weaponData.identityData.bulletPrefab, owner.transform.position).Initialize(weaponData, BulletVelocity(rad));
    }
}
