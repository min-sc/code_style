    public enum MessageType
    {
        DAMAGED,
        DEAD,
        RESPAWN,
    }

    public interface IMessageReceiver
    {
        void OnReceiveMessage(MessageType type, object sender, object msg);
    }

    //use like this

    // [EnforceType(typeof(Message.IMessageReceiver))]
    // public List<MonoBehaviour> onDamageMessageReceivers;

    // var messageType = currentHitPoints <= 0 ? MessageType.DEAD : MessageType.DAMAGED;

    // for (var i = 0; i < onDamageMessageReceivers.Count; ++i)
    // {
    //     var receiver = onDamageMessageReceivers[i] as IMessageReceiver;
    //     receiver.OnReceiveMessage(messageType, this, data);
    // }


    // public void OnReceiveMessage(Message.MessageType type, object sender, object msg)
    // {
    //     switch (type)
    //     {
    //         case Message.MessageType.DEAD:
    //             Death((Damageable.DamageMessage)msg);
    //             break;
    //         case Message.MessageType.DAMAGED:
    //             ApplyDamage((Damageable.DamageMessage)msg);
    //             break;
    //         default:
    //             break;
    //     }
    // }