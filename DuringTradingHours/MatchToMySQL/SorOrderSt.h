#ifndef SorOrderStH
#define SorOrderStH

   /// �U��n�D�y�{,�Y�@�B�J�B�z�L�{.
   enum TReqStep
   {
      /// �e�U�n�D�B�z��.
      TReqStep_Pending     = 0,

      /// �����ˬd��(���ݭ����B��s).
      TReqStep_ScChecking  = 2,
      /// �����e�U�n�D�h�^���ϥΪ�(����~�D��)�T�wor�j��.
      TReqStep_BackConfirm = 3,
      /// [����Ҧ���e] Qu��.
      TReqStep_BfQueuing   = 4,
      /// �����e�U�n�D�ƶ���.
      TReqStep_Queuing     = 5,
      /// �����e�U�n�D�ǰe��.
      TReqStep_Sending     = 6,
      /// �����e�U�n�D�w�e�X(���^��).
      TReqStep_Sent        = 7,

      /// [�t�@�xSOR]�B�z���L�{.
      TReqStep_OtherHost        = 20,
      /// [�t�@�xSOR]�B�z���e�U�n�D�h�^���ϥΪ�(����~�D��)�T�wor�j��.
      TReqStep_OtherBackConfirm = TReqStep_OtherHost + TReqStep_BackConfirm,
      /// [�t�@�xSOR] �� [����Ҧ���e] Qu��.
      TReqStep_OtherBfQueuing   = TReqStep_OtherHost + TReqStep_BfQueuing,
      /// [�t�@�xSOR]�B�z���e�U�n�D�ƶ���.
      TReqStep_OtherQueuing     = TReqStep_OtherHost + TReqStep_Queuing,
      /// [�t�@�xSOR]�B�z���e�U�n�D�ǰe��.
      TReqStep_OtherSending     = TReqStep_OtherHost + TReqStep_Sending,
      /// [�t�@�xSOR]�B�z���U�n�D�w�e�X(���^��).
      TReqStep_OtherSent        = TReqStep_OtherHost + TReqStep_Sent,

      /// ��SorApi�]�w[�B�z�L�{���^��]��,�Y���Ȯɩʪ��T��,
      /// �Ҧp: [�U�����~,���դ�...]
      TReqStep_AckMessage = 40,
      
      /// ��������(����):�Ҧp:��������檺Bid����,Offer�|���^��.
      TReqStep_PartReject = 88,
      /// ��������:�Ҧp��������檺�䤤�@��,�ٯʥt�@��.
      TReqStep_PartFinish = 89,

      /// �e�U�n�D�w����.
      TReqStep_Finished    = 90,
      /// ���ƪ�[����]�^��,�Ҧp:������,������Bid����,�A����Offer���\,����Offer���\���^���N�OTReqStep_DupFinished.
      /// �������e�U����,���|�^�е� SorApi.
      TReqStep_DupFinished = 91,

      /// �e�U�n�D���A����(�Ҧp:�e�X���_�u).
      TReqStep_UnknownFail = 95,
      /// �e�U�n�D�ڵ�.
      TReqStep_Reject      = 99,
      /// �����즨��^��,�ҫإߪ��s��(��|���`�s��^��).
      /// �p�G��|�s��,������R��,�������ϥ� [TReqKind_ChgXXX or TReqKind_Del] + [�@��y�{�B�J].
      TReqStep_RptSuggestNew = 100,

      /// �t�X TReqKind_RptFilled: ��������
      TReqStep_RptPartFilled = 110,
      /// �t�X TReqKind_RptFilled: ��������.
      TReqStep_RptFullFilled = 111,

      /// �e�U�]IOC/FOK������Ө���.
      TReqStep_RptExchgKilled = 120,
      /// �����榬��t�@�䪺����ҧR��.
      TReqStep_RptExchgKilled2 = 121,

      /// �R���浲��.
      TReqStep_ADFinished = 125,

      /// �B�J�w����,������S���ܰ�.
      /// �Ҧp: FIX �� ExecType=D(Restated).
      TReqStep_Restated   = 126,
   };

   /// �e�U�{�b���A.
   enum TOrderSt
   {
      /// �|���B�z.
      TOrderSt_NewPending = TReqStep_Pending,
      /// �s��n�D�h�^���ϥΪ�(����~�D��)�T�wor�j��.
      TOrderSt_NewBackConfirm = TReqStep_BackConfirm,
      /// �s�� [����Ҧ���e] Qu��.
      TOrderSt_NewBfQueuing   = TReqStep_BfQueuing,
      /// �s��ƶ���.
      TOrderSt_NewQueuing     = TReqStep_Queuing,
      /// �s��ǰe��.
      TOrderSt_NewSending     = TReqStep_Sending,
      /// �s��w�e�X(���^��).
      TOrderSt_NewSent        = TReqStep_Sent,

      /// �s��[�t�@�xSOR]�B�z���L�{.
      TOrderSt_OtherHost           = TReqStep_OtherHost,
      /// [�t�@�xSOR]�s��n�D�h�^���ϥΪ�(����~�D��)�T�wor�j��.
      TOrderSt_OtherNewBackConfirm = TOrderSt_OtherHost + TOrderSt_NewBackConfirm,
      /// [�t�@�xSOR] �s�� [����Ҧ���e] Qu��.
      TOrderSt_OtherNewBfQueuing   = TOrderSt_OtherHost + TOrderSt_NewBfQueuing,
      /// [�t�@�xSOR]�s��ƶ���.
      TOrderSt_OtherNewQueuing     = TOrderSt_OtherHost + TOrderSt_NewQueuing,
      /// [�t�@�xSOR]�s��ǰe��.
      TOrderSt_OtherNewSending     = TOrderSt_OtherHost + TOrderSt_NewSending,
      /// [�t�@�xSOR]�s��w�e�X(���^��).
      TOrderSt_OtherNewSent        = TOrderSt_OtherHost + TOrderSt_NewSent,


      /// �s��n�D(�Ĥ@���q)�w����,�Ҧp:
      /// - �d�B����Leg�D���w����,���|���e�������.
      /// - �d�Bĳ���ӳ��w���\,���|���e�X����.
      TOrderSt_NewFinished1 = 51,
      /// �s��n�D(�ĤG���q)�w����,�Ҧp:
      /// - ���Ӱe�X��.
      TOrderSt_NewFinished2 = 52,
      /// �H�U TOrderSt_NewFinished3..TOrderSt_NewFinished9 �̦�����,�s��U���q������.
      /// �ѦU�өe�U�Ѧۦ�w�q����.
      /// �Ҧp: �d�Bĳ���ӳ�����OK
      TOrderSt_NewFinished3 = 53,
      TOrderSt_NewFinished4 = 54,
      TOrderSt_NewFinished5 = 55,
      TOrderSt_NewFinished6 = 56,
      TOrderSt_NewFinished7 = 57,
      TOrderSt_NewFinished8 = 58,
      TOrderSt_NewFinished9 = 59,
      /// �@�ӯS��[���q�ʵ���]���A,�Ҧp:
      /// - �d�Bĳ���ӳ�,�R�����ӫ�,�^�쵥�ԩ��Ӫ��A(���A�X�ܤp)
      /// - �Φ����� [���A�X] �ݭn�ܤp�����p, �h�ϥΦ����A, �j��P�B�ҲնפJ�P�B���.
      TOrderSt_NewBackFinished = 50,


      /// �����R�� [��L�D�� NewQueuing] ���e�U.
      /// ��ڪ��R��B�z�O�b[��D�����P�B�Ҳ�]�i��P�_.
      /// �קK��D�X�w�N�s��e�X(���|���P�B����),
      /// ���t�@�D���o����R����O.
      TOrderSt_InternalCanceling = 81,
      /// �s��|���e�X�N�Q�R��.
      TOrderSt_InternalCanceled = 91,


      /// �s��n�D���A����(�Ҧp:�e�X���_�u).
      TOrderSt_NewUnknownFail = TReqStep_UnknownFail,
      /// �s��n�D�ڵ�.
      TOrderSt_NewReject = TReqStep_Reject,

      /// �e�U�w����(����Ҥw����).
      TOrderSt_Accepted  = 101,
      /// ��������.
      TOrderSt_PartFilled = TReqStep_RptPartFilled,
      /// ��������.
      TOrderSt_FullFilled = TReqStep_RptFullFilled,
      /// �@��:�e�U�]IOC/FOK������Ө���.
      /// ����:�ɶ���۰ʧR��,���s�����³����۰ʧR��.
      TOrderSt_ExchgKilled = TReqStep_RptExchgKilled,

      /// ����ұN�e�U���_, �����j���a�ϴ���үS�����A, �i�z�L[����]�ާ@�ܦ^�i���X���A.
      TOrderSt_Suspended = 130,

      /// ����������: FIX �� ExecType=3=DoneForDay.
      TOrderSt_DoneForDay = 140,
      /// �e�U�w����, �Y���򦳦��즨��, �̵M�����ܦ����A.
      /// �ثe�u�� FIX ���������\�~�|�]�w�����A.
      TOrderSt_Canceled   = 141,
   };

#endif
