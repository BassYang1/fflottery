﻿//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lottery.FFData
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TicketEntities : DbContext
    {
        public TicketEntities()
            : base("name=TicketEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Act_ActiveNews> Act_ActiveNews { get; set; }
        public DbSet<Act_ActiveRecord> Act_ActiveRecord { get; set; }
        public DbSet<Act_ActiveSet> Act_ActiveSet { get; set; }
        public DbSet<Act_AgentFHRecord> Act_AgentFHRecord { get; set; }
        public DbSet<Act_AutoRecord> Act_AutoRecord { get; set; }
        public DbSet<Act_BetRecond> Act_BetRecond { get; set; }
        public DbSet<Act_Day15FHSet> Act_Day15FHSet { get; set; }
        public DbSet<Act_DayGzSet> Act_DayGzSet { get; set; }
        public DbSet<Act_DayYJSet> Act_DayYJSet { get; set; }
        public DbSet<Act_SetChargeDetail> Act_SetChargeDetail { get; set; }
        public DbSet<Act_SetFHDetail> Act_SetFHDetail { get; set; }
        public DbSet<Act_SetGZDetail> Act_SetGZDetail { get; set; }
        public DbSet<Act_SetGZDetail2> Act_SetGZDetail2 { get; set; }
        public DbSet<Act_SetYJDetail> Act_SetYJDetail { get; set; }
        public DbSet<Act_SetYJDetail2> Act_SetYJDetail2 { get; set; }
        public DbSet<Act_UserFHDetail> Act_UserFHDetail { get; set; }
        public DbSet<Log_AdminOper> Log_AdminOper { get; set; }
        public DbSet<log_Bet> log_Bet { get; set; }
        public DbSet<Log_ContractOper> Log_ContractOper { get; set; }
        public DbSet<Log_Exception> Log_Exception { get; set; }
        public DbSet<Log_Point> Log_Point { get; set; }
        public DbSet<Log_Sys> Log_Sys { get; set; }
        public DbSet<Log_UserLogin> Log_UserLogin { get; set; }
        public DbSet<N_Merchant> N_Merchant { get; set; }
        public DbSet<N_User> N_User { get; set; }
        public DbSet<N_UserBank> N_UserBank { get; set; }
        public DbSet<N_UserBankLog> N_UserBankLog { get; set; }
        public DbSet<N_UserBet> N_UserBet { get; set; }
        public DbSet<N_UserCharge> N_UserCharge { get; set; }
        public DbSet<N_UserChargeLog> N_UserChargeLog { get; set; }
        public DbSet<N_UserContract> N_UserContract { get; set; }
        public DbSet<N_UserContractDetail> N_UserContractDetail { get; set; }
        public DbSet<N_UserEmail> N_UserEmail { get; set; }
        public DbSet<N_UserGetCash> N_UserGetCash { get; set; }
        public DbSet<N_UserGetCashHistory> N_UserGetCashHistory { get; set; }
        public DbSet<N_UserGroup> N_UserGroup { get; set; }
        public DbSet<N_UserGroupQuota> N_UserGroupQuota { get; set; }
        public DbSet<N_UserLevel> N_UserLevel { get; set; }
        public DbSet<N_UserMessage> N_UserMessage { get; set; }
        public DbSet<N_UserMoneyLog> N_UserMoneyLog { get; set; }
        public DbSet<N_UserMoneyStatAll> N_UserMoneyStatAll { get; set; }
        public DbSet<N_UserPlaySetting> N_UserPlaySetting { get; set; }
        public DbSet<N_UserPointQuota> N_UserPointQuota { get; set; }
        public DbSet<N_UserQuota> N_UserQuota { get; set; }
        public DbSet<N_UserQuotas> N_UserQuotas { get; set; }
        public DbSet<N_UserRegLink> N_UserRegLink { get; set; }
        public DbSet<N_UserZhBet> N_UserZhBet { get; set; }
        public DbSet<Pay_temp> Pay_temp { get; set; }
        public DbSet<PayUrl_temp> PayUrl_temp { get; set; }
        public DbSet<Sys_Admin> Sys_Admin { get; set; }
        public DbSet<Sys_AutoRank> Sys_AutoRank { get; set; }
        public DbSet<Sys_Bank> Sys_Bank { get; set; }
        public DbSet<Sys_BankBinInfo> Sys_BankBinInfo { get; set; }
        public DbSet<Sys_ChargeSet> Sys_ChargeSet { get; set; }
        public DbSet<Sys_Info> Sys_Info { get; set; }
        public DbSet<Sys_LoginCheck> Sys_LoginCheck { get; set; }
        public DbSet<Sys_Lottery> Sys_Lottery { get; set; }
        public DbSet<Sys_LotteryCheck> Sys_LotteryCheck { get; set; }
        public DbSet<Sys_LotteryData> Sys_LotteryData { get; set; }
        public DbSet<Sys_LotteryPlaySetting> Sys_LotteryPlaySetting { get; set; }
        public DbSet<Sys_LotteryTime> Sys_LotteryTime { get; set; }
        public DbSet<Sys_lotteryUrl> Sys_lotteryUrl { get; set; }
        public DbSet<Sys_Menu> Sys_Menu { get; set; }
        public DbSet<Sys_Message> Sys_Message { get; set; }
        public DbSet<Sys_News> Sys_News { get; set; }
        public DbSet<Sys_PlayBigType> Sys_PlayBigType { get; set; }
        public DbSet<Sys_PlaySmallType> Sys_PlaySmallType { get; set; }
        public DbSet<Sys_Role> Sys_Role { get; set; }
        public DbSet<Sys_SbfChannelMap> Sys_SbfChannelMap { get; set; }
        public DbSet<Sys_TaskSet> Sys_TaskSet { get; set; }
    }
}
