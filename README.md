# .Net Core学习计划
## 1、异步的特性
异步方法使用async关键字进行定义，并通过await关键字来等待异步操作完成。
<br><br>
异步方法通常返回Task或Task\<T\>，其中Task用于返回具有特定类型的值的异步操作。Task表示一个没有返回值的任务，而Task\<T\>则是一个带有泛型类型参数的任务，该参数表示返回值的类型。
