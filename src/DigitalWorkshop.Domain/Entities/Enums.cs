namespace DigitalWorkshop.Domain.Enums
{
    public enum TpStatus
    {
        Draft,          // Черновик
        InReview,       // На согласовании
        Approved,       // Утвержден
        Archived        // Архив (старая версия)
    }

    public enum UserRole
    {
        Technologist,       // Создает/Редактирует черновик
        LeadTechnologist,   // Согласует
        ChiefEngineer,      // Финальное утверждение
        Operator,           // Исполняет
        QualityControl,     // ОТК
        Storekeeper         // Кладовщик
    }
}