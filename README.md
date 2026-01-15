# MornEase

## 概要

イージング関数を提供するライブラリ。0〜1の正規化された値に対して様々なイージングタイプを適用。

## 依存関係

| 種別 | 名前 |
|------|------|
| 外部パッケージ | なし |
| Mornライブラリ | なし |

## 使い方

### 基本的な使用方法

```csharp
float normalizedProgress = 0.5f;  // 0〜1の値
float easedValue = normalizedProgress.Ease(MornEaseType.EaseInOutCubic);
```

### 提供されるイージングタイプ（31種類）

| カテゴリ | タイプ |
|---------|--------|
| 線形 | Linear |
| 三角関数 | EaseInSine, EaseOutSine, EaseInOutSine |
| 多項式(2乗) | EaseInQuad, EaseOutQuad, EaseInOutQuad |
| 多項式(3乗) | EaseInCubic, EaseOutCubic, EaseInOutCubic |
| 多項式(4乗) | EaseInQuart, EaseOutQuart, EaseInOutQuart |
| 多項式(5乗) | EaseInQuint, EaseOutQuint, EaseInOutQuint |
| 指数 | EaseInExpo, EaseOutExpo, EaseInOutExpo |
| 円形 | EaseInCirc, EaseOutCirc, EaseInOutCirc |
| バック | EaseInBack, EaseOutBack, EaseInOutBack |
| 弾力 | EaseInElastic, EaseOutElastic, EaseInOutElastic |
| バウンス | EaseInBounce, EaseOutBounce, EaseInOutBounce |
